using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcelotAPIMonitoring.Monitoring_Entities
{
    public class FileLoggerMiddleware
    {
        private readonly ILogger<FileLoggerMiddleware> _logger;
        private readonly RequestDelegate _next;

        public FileLoggerMiddleware(RequestDelegate next, ILogger<FileLoggerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Request.EnableBuffering();

            var builder = new StringBuilder();
            var request = await FormatRequest(context.Request);

            builder.Append("Request: ").AppendLine(request);
            builder.AppendLine("Request headers: ");
            foreach (var header in context.Request.Headers)
            {
                builder.Append(header.Key).Append(':').AppendLine(header.Value);
            }

            var orginalBodyStream = context.Response.Body;

            using var responseBody = new MemoryStream();

            context.Response.Body = responseBody;

            await _next(context);

            var response = await FormatResponse(context.Response);
            builder.Append("Response: ").AppendLine(response);
            builder.AppendLine("Response headers:");
            foreach (var header in context.Response.Headers)
            {
                builder.Append(header.Key).Append(':').AppendLine(header.Value);
            }

            _logger.LogInformation(builder.ToString());

            await responseBody.CopyToAsync(orginalBodyStream);
        }

        public async Task<string> FormatRequest(HttpRequest request)
        {
            using var reader = new StreamReader(
                request.Body,
                encoding: Encoding.UTF8,
                detectEncodingFromByteOrderMarks: false,
                leaveOpen: true);
            var body = await reader.ReadToEndAsync();

            var formattedRequest = $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {body}";

            request.Body.Position = 0;

            return formattedRequest;
        }

        public async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);

            string text = await new StreamReader(response.Body).ReadToEndAsync();

            response.Body.Seek(0, SeekOrigin.Begin);

            return $"{response.StatusCode}: {text}";
        }

        
    }
}
