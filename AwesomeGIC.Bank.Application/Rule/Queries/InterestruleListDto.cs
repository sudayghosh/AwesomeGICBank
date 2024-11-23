using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeGIC.Bank.Application.Rule.Queries
{
    public class InterestRuleListDto
    {
        public string RuleId { get; set; }

        public DateTime DateTime { get; set; }

        public decimal Rate { get; set; }
    }
}
