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
    public class InterestRuleConfiguration : IEntityTypeConfiguration<InterestRule>
    {
        public void Configure(EntityTypeBuilder<InterestRule> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.RuleId).HasMaxLength(100).IsRequired();
            builder.Property(e => e.DateTime).IsRequired();
            builder.Property(e => e.CreateDateTime).IsRequired();
            builder.Property(e => e.Rate).HasPrecision(5, 4).IsRequired();
        }
    }
}
