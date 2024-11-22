using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeGIC.Bank.Domain.Entities
{
    public class Transaction
    {
        public Guid Id { get; set; }

        public string TxnId { get; set; }

        public DateTime TxnDate { get; set; }

        public string Type { get; set; }

        public decimal Amount { get; set; }

        public Account Account { get; set; }
    }
}
