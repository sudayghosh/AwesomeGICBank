using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeGIC.Bank.UI.Dto
{
    public class InterestRuleReqDto
    {
        public string RuleId { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        [RegularExpression(@"^\d{4}(0[1-9]|1[0-2])(0[1-9]|[12]\d|3[01])$", ErrorMessage = "Date must be in YYYYMMdd format eg. 20240605.")]
        public string DateTime { get; set; }

        [Required(ErrorMessage = "Rate is required.")]
        [Range(0.01, 99.99, ErrorMessage = "Interest rate must be greater than 0 and less than 100.")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$", ErrorMessage = "Decimals are allowed up to 2 decimal places.")]
        public decimal Rate { get; set; }
    }
}
