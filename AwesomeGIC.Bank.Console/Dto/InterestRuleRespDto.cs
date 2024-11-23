using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeGIC.Bank.UI.Dto
{
    public class InterestRuleRespDto
    {
        public ErrorResponse? ErrorResponse { get; set; }

        public List<InterestRuleDetailsRespDto> Rules { get; set; }
    }

    public class InterestRuleDetailsRespDto
    {
        public string RuleId { get; set; }

        public DateTime DateTime { get; set; }

        public decimal Rate { get; set; }
    }
}
