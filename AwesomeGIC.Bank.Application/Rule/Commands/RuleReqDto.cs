using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeGIC.Bank.Application.Rule.Commands
{
    public class RuleReqDto
    {
        public string RuleId { get; set; }

        public string DateTime { get; set; }

        public decimal Rate { get; set; }
    }
}
