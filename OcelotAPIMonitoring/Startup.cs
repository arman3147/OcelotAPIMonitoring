using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;
using OcelotAPIMonitoring.Monitoring_Entities;
using Ocelot.Middleware;
using OcelotAPIMonitoring.Monitoring_Middlewares;
using OcelotAPIMonitoring.Repository.Interfaces;
using OcelotAPIMonitoring.Repository;
using Microsoft.EntityFrameworkCore;

namespace OcelotAPIMonitoring
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOcelot();
            services.AddScoped<ILoggerRepository, LoggerRepository>();
            services.AddDbContext<LoggerDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("LoggerConnectionString")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();

            app.UseMiddleware<IPConfigurationMiddleware>();
            app.UseMiddleware<FileLoggerMiddleware>();
            app.UseMiddleware<DBLoggerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            await app.UseOcelot();
        }
    }
}
