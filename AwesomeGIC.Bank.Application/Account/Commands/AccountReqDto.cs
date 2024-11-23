using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeGIC.Bank.Application.Account.Commands
{
    public class AccountReqDto
    {
        public string AccountNo { get; set; }

        public string TxnDate { get; set; }

        public string Type { get; set; }

        public decimal Amount { get; set; }
    }
}
