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
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.TxnId).HasMaxLength(100).IsRequired();
            builder.Property(e => e.TxnDate).IsRequired();
            builder.Property(e => e.Type).HasMaxLength(1).IsRequired();
            builder.Property(e => e.Amount).HasPrecision(12, 6).IsRequired();
        }
    }
}
