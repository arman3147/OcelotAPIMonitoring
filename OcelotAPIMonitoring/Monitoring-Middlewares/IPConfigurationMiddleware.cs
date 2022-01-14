using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OcelotAPIMonitoring.Monitoring_Middlewares
{
    public class IPConfigurationMiddleware
    {
        private readonly RequestDelegate _next;

        public IPConfigurationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue("X-Forwarded-For", out StringValues value))
                context.Request.Headers.Add("SourceIP", context.Connection.RemoteIpAddress.ToString());

            await _next(context);
        }
    }
}
