using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace Api
{
    /// <summary>
    /// Main entry
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            // This can be reviewed on Azure's application insights
            System.Diagnostics.Trace.TraceInformation("Application server starting");

            var host = WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .ConfigureLogging(logging => logging.SetMinimumLevel(LogLevel.Error))
                .UseStartup<Startup>()
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    logging.AddConsole();
                    logging.AddDebug();
                    logging.AddEventSourceLogger();
                })
                .Build();
            
            host.Run();
        }
    }
}
