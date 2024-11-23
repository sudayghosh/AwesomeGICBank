using AwesomeGIC.Bank.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeGIC.Bank.Domain.Entities
{
    public class Transaction : KeyedEntity
    {
        public string TxnId { get; set; }

        public DateTime TxnDate { get; set; }

        public string Type { get; set; }

        public decimal Amount { get; set; }

        public decimal Balance { get; set; }

        public Account Account { get; set; }
    }
}
