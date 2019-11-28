using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using Api.Configs;
using Api.Extensions;
using Dal.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Models.Constants;
using Models.Entities.Users;
using OwaspHeaders.Core.Extensions;
using OwaspHeaders.Core.Models;
using StructureMap;
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
            // If environment is localhost, then enable CORS policy, otherwise no cross-origin access
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            // Add framework services
            // Add functionality to inject IOptions<T>
            services.AddOptions();
            services.Configure<JwtSettings>(_configuration.GetSection("JwtSettings"));


            // Add our Config object so it can be injected
            services.Configure<SecureHeadersMiddlewareConfiguration>(
                _configuration.GetSection("SecureHeadersMiddlewareConfiguration"));

            services.AddLogging();

            services.AddRouting(options => { options.LowercaseUrls = true; });

            if (_env.IsDevelopment())
            {
                services.AddDistributedMemoryCache();
            }
            else
            {
                services.AddDistributedRedisCache(opt =>
                {
                    opt.Configuration = Environment.GetEnvironmentVariable("REDISCLOUD_URL");
                });
            }

            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromMinutes(50);
                options.Cookie.HttpOnly = true;
                options.Cookie.Name = ApiConstants.ApplicationName;
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Contractor-Finder-Api",
                    Description = "Contractor finder service API layer, .NET Core + PostgresSQL"
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
              
                c.AddSecurityDefinition("Bearer", // Name the security scheme
                    new OpenApiSecurityScheme {
                        Description = "JWT Authorization header using the Bearer scheme.",
                        Type = SecuritySchemeType.Http, //We set the scheme type to http since we're using bearer authentication
                        Scheme = "bearer" //The name of the HTTP Authorization scheme to be used in the Authorization header. In this case "bearer".
                    });
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
                })
                .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()))
                .AddRazorPagesOptions(x => x.Conventions.ConfigureFilter(new IgnoreAntiforgeryTokenAttribute()));

            services.AddDbContext<EntityDbContext>(builder =>
            {
                if (_env.IsDevelopment())
                {
                    builder.UseSqlite(_configuration.GetValue<string>("ConnectionStrings:Sqlite"));
                }
                else
                {
                    builder.UseNpgsql(
                        ConnectionStringUrlToResource(Environment.GetEnvironmentVariable("DATABASE_URL"))
                        ?? throw new Exception("DATABASE_URL is null"));
                }
            });

            services.AddIdentity<User, UserRole>(opt => opt.User.RequireUniqueEmail = true)
                .AddEntityFrameworkStores<EntityDbContext>()
                .AddDefaultTokenProviders();

            var jwtSetting = _configuration
                .GetSection("JwtSettings")
                .Get<JwtSettings>();

            services.AddAuthentication(opt => {
                    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddCookie(x => x.Cookie.MaxAge = TimeSpan.FromMinutes(60))
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

            var container = new Container(config =>
            {
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

            container.AssertConfigurationIsValid();

            return container.GetInstance<IServiceProvider>();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        public void Configure(IApplicationBuilder app)
        {
            // Add SecureHeadersMiddleware to the pipeline
            app.UseSecureHeadersMiddleware(_configuration.Get<SecureHeadersMiddlewareConfiguration>());

            app.UseCors("CorsPolicy")
                .UseEnableRequestRewind()
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

            // Use wwwroot folder as default static path
            app.UseDefaultFiles()
                .UseStaticFiles()
                .UseCookiePolicy()
                .UseSession()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints => endpoints.MapControllers());

            Console.WriteLine("Application Started!");
        }
    }
}