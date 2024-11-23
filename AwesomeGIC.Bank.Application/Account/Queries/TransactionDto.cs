using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeGIC.Bank.Application.Account.Queries
{
    public class TransactionDto
    {
        public string TxnId { get; set; }

        public DateTime TxnDate { get; set; }

        public string Type { get; set; }

        public decimal Amount { get; set; }

        public decimal Balance { get; set; }
    }
}
