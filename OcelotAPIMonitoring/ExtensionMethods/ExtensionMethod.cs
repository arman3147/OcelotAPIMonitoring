using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OcelotAPIMonitoring.ExtensionMethods
{
    public static class ExtensionMethod
    {
        public static async Task<object> FormatRequestAsync(this HttpRequest request)
        {
            using var reader = new StreamReader(
                request.Body,
                encoding: Encoding.UTF8,
                detectEncodingFromByteOrderMarks: false,
                leaveOpen: true);
            var body = await reader.ReadToEndAsync();

            request.Body.Position = 0;

            return new 
                {
                    URL = $"{request.Scheme}:// {request.Host}{request.Path} {request.QueryString}",
                    Body = body
            };
        }

        public static async Task<object> FormatResponseAsync(this HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);

            string text = await new StreamReader(response.Body).ReadToEndAsync();

            response.Body.Seek(0, SeekOrigin.Begin);

            return  new
                {
                    StatusCode = response.StatusCode,
                    Response = text
                };
        }

        public static  string ToJson(this object input)
        {
            return  JsonConvert.SerializeObject(input);
        }

        public static string ToPersian(this DateTime dateTime)
        {
            PersianCalendar calendar = new PersianCalendar();
            return $"{calendar.GetYear(dateTime)}/{calendar.GetMonth(dateTime)}/{calendar.GetDayOfMonth(dateTime)}," +
                   $"{calendar.GetHour(dateTime)}:{calendar.GetMinute(dateTime)}:{calendar.GetSecond(dateTime)}:{calendar.GetMilliseconds(dateTime)}";
        }
    }
}
