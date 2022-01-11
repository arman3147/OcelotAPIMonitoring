using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using OcelotAPIMonitoring.Monitoring_Entities;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OcelotAPIMonitoring
{
    public class Program
    {
        public static IWebHost BuildWebHost(string[] args)
        {
            var builder = WebHost.CreateDefaultBuilder(args);

            builder.ConfigureAppConfiguration(
                    ic => ic.AddJsonFile(Path.Combine("Configuration",
                                                      "configuration.json")))
                   .ConfigureServices(
                        s =>
                        {
                            s.AddSingleton(builder);
                            s.AddOcelot();
                        }
                    )
                   .UseStartup<Startup>()
                   .UseSerilog((_, config) =>
                   {
                       config
                        .MinimumLevel.Information()
                        .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
                        .Enrich.FromLogContext()
                        .WriteTo.File(@"Logs\log.txt", rollingInterval: RollingInterval.Day);
                   }
                   ).Configure(app =>
                   {
                       app.UseMiddleware<LoggingMiddleware>();
                       app.UseOcelot().Wait();
                   }
                   );
            var host = builder.Build();
            return host;
        }
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }
    }
}
