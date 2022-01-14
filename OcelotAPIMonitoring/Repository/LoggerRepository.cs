using Microsoft.EntityFrameworkCore;
using Nest;
using OcelotAPIMonitoring.Models;
using OcelotAPIMonitoring.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OcelotAPIMonitoring.Repository
{
    public class LoggerRepository :ILoggerRepository
    {
        private LoggerDbContext context;

        public LoggerRepository(LoggerDbContext context)
        {
            this.context = context;
        }

        public async Task Add(HttpContextLog log)
        {
            try
            {
                await context.AddAsync(log);
                context.SaveChanges();
            }
            catch(Exception ex)
            {

            }

        }
    }
}
