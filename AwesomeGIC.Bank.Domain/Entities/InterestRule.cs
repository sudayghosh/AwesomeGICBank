using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeGIC.Bank.Domain.Entities
{
    public class InterestRule
    {
        public InterestRule()
        {
            //Transactions = new HashSet<Transaction>();
        }

        public Guid Id { get; set; }

        public string RuleId { get; set; }

        public DateTime DateTime { get; set; }

        public DateTime CreateDateTime { get; set; }

        public decimal Rate { get; set; }
    }
}
