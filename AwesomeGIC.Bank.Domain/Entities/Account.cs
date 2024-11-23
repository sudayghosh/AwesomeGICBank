using AwesomeGIC.Bank.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeGIC.Bank.Domain.Entities
{
    public class Account : KeyedEntity
    {
        public Account()
        { 
            Transactions = new HashSet<Transaction>();
        }

        public string AccountNo { get; set; }

        public ICollection<Transaction> Transactions { get; set; }
    }
}
