using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OcelotAPIMonitoring.Models
{
    public class HttpContextLog
    {
        public Guid Id { get; set; }

        public string UpstreamGatewayURL { get; set; }

        public string RequestHeaders { get; set; }

        public string RequestBody { get; set; }

        public string ResponseHeaders { get; set; }

        public string ResponseBody { get; set; }

        public string RequestTime { get; set; }
    }
}
