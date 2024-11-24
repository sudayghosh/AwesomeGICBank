using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeGIC.Bank.Application.Account.Commands
{
    public class AccountTransactionReqDto
    {
        public string AccountNo { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        [RegularExpression(@"^\d{4}(0[1-9]|1[0-2])(0[1-9]|[12]\d|3[01])$", ErrorMessage = "Date must be in YYYYMMdd format.")]
        public string TxnDate { get; set; }
    }
}
