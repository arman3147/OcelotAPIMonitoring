using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OcelotAPIMonitoring.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OcelotAPIMonitoring.DbContexts.DbContextConfiguration
{
    public class LoggerDbContextEntityTypeConfiguration : IEntityTypeConfiguration<HttpContextLog>
    {
        public void Configure(EntityTypeBuilder<HttpContextLog> builder)
        {
            builder.HasKey(h => h.Id);
        }
    }
}
