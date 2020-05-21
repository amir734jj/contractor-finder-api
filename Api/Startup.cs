using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Api.Configs;
using Api.Extensions;
using Api.Middleware;
using Dal.Configs;
using Dal.Interfaces;
using Dal.Services.LiteDb;
using Dal.Utilities;
using EFCache;
using EFCache.Redis;
using LiteDB;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Models.Constants;
using Models.Entities.Users;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using OwaspHeaders.Core.Extensions;
using OwaspHeaders.Core.Models;
using StackExchange.Redis;
using StructureMap;
using Swashbuckle.AspNetCore.JsonMultipartFormDataSupport;
using static Api.Utilities.ConnectionStringUtility;

namespace Api
{
    public class Startup
    {
        private readonly IConfigurationRoot _configuration;

        private readonly IWebHostEnvironment _env;

        public Startup(IWebHostEnvironment env)
        {
            _env = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddJsonFile("secureHeaderSettings.json", true, true)
                .AddEnvironmentVariables();

            _configuration = builder.Build();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // If using Kestrel:
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            // If using IIS:
            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
            
            services.AddHttpsRedirection(options => { options.HttpsPort = 443; });

            services.AddDistributedMemoryCache();
            
            // If environment is localhost, then enable CORS policy, otherwise no cross-origin access
            services.AddCors(options => options.AddPolicy("CorsPolicy", builder => builder
                .WithOrigins(_configuration.GetSection("TrustedSpaUrls").Get<string[]>())
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()));

            // Add framework services
            // Add functionality to inject IOptions<T>
            services.AddOptions();
            services.Configure<JwtSettings>(_configuration.GetSection("JwtSettings"));

            // Add our Config object so it can be injected
            services.Configure<SecureHeadersMiddlewareConfiguration>(
                _configuration.GetSection("SecureHeadersMiddlewareConfiguration"));

            services.AddLogging();

            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromMinutes(50);
                options.Cookie.HttpOnly = true;
                options.Cookie.Name = ApiConstants.ApplicationName;
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            });

            services.AddDbContext<EntityDbContext>(builder =>
            {
                if (_env.IsDevelopment())
                {
                    builder.UseSqlite(_configuration.GetValue<string>("ConnectionStrings:Sqlite"));
                }
                else
                {
                    builder.UseNpgsql(
                        ConnectionStringUrlToResource(_configuration.GetRequiredValue<string>("DATABASE_URL")));
                }
            });

            services.AddIdentity<User, UserRole>(opt => opt.User.RequireUniqueEmail = true)
                .AddRoles<UserRole>()
                .AddEntityFrameworkStores<EntityDbContext>()
                .AddDefaultTokenProviders();

            if (_env.IsDevelopment())
            {
                EntityFrameworkCache.Initialize(new InMemoryCache());
            }
            else
            {
                var redisCacheConfig = ConfigurationOptions.Parse(_configuration.GetValue<string>("REDISTOGO_URL"));

                redisCacheConfig.AbortOnConnectFail = false;    // Needed otherwise redis client will fail
                
                EntityFrameworkCache.Initialize(new RedisCache(redisCacheConfig));
            }

            var jwtSetting = _configuration
                .GetSection("JwtSettings")
                .Get<JwtSettings>();

            services.AddAuthentication(options => {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(config =>
                {
                    config.RequireHttpsMetadata = false;
                    config.SaveToken = true;

                    config.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = jwtSetting.Issuer,
                        ValidAudience = jwtSetting.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting.Key))
                    };
                });

            services.AddControllers(opt =>
                {
                    opt.EnableEndpointRouting = false;

                    opt.ModelValidatorProviders.Clear();

                    // Not need to have https
                    opt.RequireHttpsPermanent = false;

                    // Allow anonymous for localhost
                    if (_env.IsDevelopment())
                    {
                        opt.Filters.Add<AllowAnonymousFilter>();
                    }

                    opt.Filters.Add<CustomExceptionFilterAttribute>();
                })
                .AddNewtonsoftJson(option =>
                {
                    option.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    option.SerializerSettings.Converters.Add(new StringEnumConverter());
                });

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor |
                                           ForwardedHeaders.XForwardedProto;
                options.KnownNetworks.Clear();
                options.KnownProxies.Clear();
            });

            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Contractor-Finder-Api",
                    Description = "Contractor finder service API layer, .NET Core + PostgresSQL"
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                if (File.Exists(xmlPath))
                {
                    config.IncludeXmlComments(xmlPath);
                }

                config.OperationFilter<MultiPartJsonOperationFilter>();

                config.AddSecurityDefinition("Bearer", // Name the security scheme
                    new OpenApiSecurityScheme
                    {
                        Description = "JWT Authorization header using the Bearer scheme.",
                        Type = SecuritySchemeType.Http, // We set the scheme type to http since we're using bearer authentication
                        Scheme = "bearer" // The name of the HTTP Authorization scheme to be used in the Authorization header. In this case "bearer".
                    });
            });

            services.AddJsonMultipartFormDataSupport(JsonSerializerChoice.Newtonsoft);

            var container = new Container(config =>
            {
                config.For<JwtSettings>().Use(jwtSetting);

                if (_env.IsDevelopment())
                {
                    config.For<ILiteDatabase>().Use("LiteDatabase", () => new LiteDatabase("local.lite.db"));
                    config.For<IFileService>().Use<LiteDbFileService>().Singleton();
                }
                else
                {
                    var (accessKeyId, secretAccessKey, url) = (
                        _configuration.GetRequiredValue<string>("CLOUDCUBE_ACCESS_KEY_ID"),
                        _configuration.GetRequiredValue<string>("CLOUDCUBE_SECRET_ACCESS_KEY"),
                        _configuration.GetRequiredValue<string>("CLOUDCUBE_URL")
                    );

                    var prefix = new Uri(url).Segments.Skip(1).FirstOrDefault() ?? throw new Exception("S3 url is malformed");
                    const string bucketName = "cloud-cube";

                    // Generally bad practice
                    var credentials = new BasicAWSCredentials(accessKeyId, secretAccessKey);

                    // Create S3 client
                    config.For<IAmazonS3>().Use(() => new AmazonS3Client(credentials, RegionEndpoint.USEast1));
                    config.For<S3ServiceConfig>().Use(new S3ServiceConfig(bucketName, prefix));
                }

                // Register stuff in container, using the StructureMap APIs...
                config.Scan(_ =>
                {
                    _.AssemblyContainingType(typeof(Startup));
                    _.Assembly("Logic");
                    _.Assembly("Dal");
                    _.WithDefaultConventions();
                });

                // Populate the container using the service collection
                config.Populate(services);
            });

            // container.AssertConfigurationIsValid();

            return container.GetInstance<IServiceProvider>();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        public void Configure(IApplicationBuilder app)
        {
            // Add SecureHeadersMiddleware to the pipeline
            // See: https://github.com/GaProgMan/OwaspHeaders.Core
            app.UseSecureHeadersMiddleware(SecureHeadersMiddlewareBuilder
                .CreateBuilder()
                .Build());

            app.UseEnableRequestRewind()
                .UseDeveloperExceptionPage();

            if (_env.IsDevelopment())
            {
                app.UseDatabaseErrorPage();

                // Enable middleware to serve generated Swagger as a JSON endpoint.
                app.UseSwagger();

                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
                // specifying the Swagger JSON endpoint.
                app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });
            }

            // Not necessary for this workshop but useful when running behind kubernetes
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                // Read and use headers coming from reverse proxy: X-Forwarded-For X-Forwarded-Proto
                // This is particularly important so that HttpContent.Request.Scheme will be correct behind a SSL terminating proxy
                ForwardedHeaders = ForwardedHeaders.XForwardedFor |
                                   ForwardedHeaders.XForwardedProto
            });

            // Use wwwroot folder as default static path
            app.UseDefaultFiles()
                .UseStaticFiles()
                .UseCookiePolicy()
                .UseSession()
                .UseRouting()
                .UseCors("CorsPolicy")
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());

            Console.WriteLine("Application Started!");
        }
    }
}
