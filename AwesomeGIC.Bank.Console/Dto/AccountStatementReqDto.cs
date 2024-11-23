using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeGIC.Bank.UI.Dto
{
    public class AccountStatementReqDto
    {
        public string AccountNo { get; set; }

        [Required(ErrorMessage = "Date is required.")]
        [RegularExpression(@"^(19|20)\d{2}(0[1-9]|1[0-2])$", ErrorMessage = "Date must be in YYYYMM format.")]
        public string TxnDate { get; set; }
    }
}
