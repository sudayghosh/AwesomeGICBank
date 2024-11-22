using AwesomeGIC.Bank.Application;
using AwesomeGIC.Bank.Application.Helper;
using AwesomeGIC.Bank.Domain.Entities;
using AwesomeGIC.Bank.Infrastructure.Sql.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AwesomeGIC.Bank.Infrastructure.Sql
{
    public class AwesomeGICBankDBContext : DbContext, IAwesomeGICBankDBContext
    {
        public AwesomeGICBankDBContext(DbContextOptions<AwesomeGICBankDBContext> options) : base(options)
        {
        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var converter = new ValueConverter<string, string>(
                encryptedData => EncryptionHelper.EncryptedData(encryptedData),
                decryptedData => EncryptionHelper.DecryptedData(decryptedData));

            modelBuilder.ApplyConfiguration(new AccountConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());
            modelBuilder.ApplyConfiguration(new InterestRuleConfiguration());

            //modelBuilder.Entity<Employee>().Property(e => e.FirstName)
            //    .HasConversion(converter);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<InterestRule> InterestRules { get; set; }
    }
}
