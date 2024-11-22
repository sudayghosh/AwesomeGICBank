using AwesomeGIC.Bank.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AwesomeGIC.Bank.Application
{
    public interface IAwesomeGICBankDBContext
    {
        DbSet<AwesomeGIC.Bank.Domain.Entities.Account> Accounts { get; set; }

        DbSet<Transaction> Transactions { get; set; }

        DbSet<InterestRule> InterestRules { get; set; }


        //public DbSet<Employee> Employees { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
