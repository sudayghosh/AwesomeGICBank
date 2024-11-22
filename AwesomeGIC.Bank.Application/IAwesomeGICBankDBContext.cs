using AwesomeGIC.Bank.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AwesomeGIC.Bank.Application
{
    public interface IAwesomeGICBankDBContext
    {
        public DbSet<Employee> Employees { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
