using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeGIC.Bank.Domain.Entities
{
    public class Account
    {
        public Account()
        { 
            Transactions = new HashSet<Transaction>();
        }

        public Guid Id { get; set; }

        public string AccountNo { get; set; }

        public DateTime CreateDateTime { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}
