using OcelotAPIMonitoring.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OcelotAPIMonitoring.Repository.Interfaces
{
    public interface ILoggerRepository
    {
        public Task Add(HttpContextLog log);
    }
}
