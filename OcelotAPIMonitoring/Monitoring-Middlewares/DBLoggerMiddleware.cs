using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using OcelotAPIMonitoring.ExtensionMethods;
using OcelotAPIMonitoring.Models;
using OcelotAPIMonitoring.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OcelotAPIMonitoring.Monitoring_Middlewares
{
    public class DBLoggerMiddleware
    {
        private readonly RequestDelegate _next;

        public DBLoggerMiddleware( RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ILoggerRepository logger)
        {
            HttpContextLog log = new HttpContextLog();
            context.Request.EnableBuffering();
            log.Id = Guid.NewGuid();

            log.UpstreamGatewayURL = $"{context.Request.Scheme}:// {context.Request.Host}{context.Request.Path} {context.Request.QueryString}";
            log.RequestHeaders = context.Request.Headers.ToJson();
            log.RequestBody = context.Request.FormatRequestAsync().ToJson();

            var orginalBodyStream = context.Response.Body;

            using var responseBody = new MemoryStream();

            await _next(context);

            log.ResponseHeaders = context.Response.Headers.ToJson();
            log.ResponseBody = context.Response.FormatResponseAsync().ToJson();

            log.RequestTime = DateTime.Now.ToPersian();

            await logger.Add(log);
        }
    }
}
