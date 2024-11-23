using AwesomeGIC.Bank.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeGIC.Bank.Domain.Entities
{

    public class InterestRule : KeyedEntity
    {
        public InterestRule()
        {
        }

        public string RuleId { get; set; }

        public DateTime DateTime { get; set; }

        public decimal Rate { get; set; }
    }
}
