using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeGIC.Bank.UI.Dto
{
    public class AccountReqDto
    {
        public string AccountNo { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        [RegularExpression(@"^\d{4}(0[1-9]|1[0-2])(0[1-9]|[12]\d|3[01])$", ErrorMessage = "Date must be in YYYYMMdd format eg. 20240605.")]
        public string TxnDate { get; set; }

        [RegularExpression("^(W|D)$", ErrorMessage = "Transaction type must be either 'W' or 'D'.")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Amount is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Decimals are allowed up to 2 decimal places.")]
        public decimal Amount { get; set; }
    }
}
