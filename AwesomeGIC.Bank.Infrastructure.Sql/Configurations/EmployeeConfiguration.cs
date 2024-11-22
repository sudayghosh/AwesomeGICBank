using AwesomeGIC.Bank.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeGIC.Bank.Infrastructure.Sql.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.FirstName).HasMaxLength(200).IsRequired();
            builder.Property(e => e.LastName).HasMaxLength(200).IsRequired();
            builder.Property(e => e.Gender).HasMaxLength(50).IsRequired();
        }
    }
}
