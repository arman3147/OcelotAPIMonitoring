using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OcelotAPIMonitoring.DbContexts.DbContextConfiguration;
using OcelotAPIMonitoring.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OcelotAPIMonitoring
{
    public class LoggerDbContext:DbContext
    {
        private readonly IConfiguration _configuration;
        public LoggerDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public DbSet<HttpContextLog> HttpContextLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("LoggerConnectionString"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new LoggerDbContextEntityTypeConfiguration());
        }
    }
}
