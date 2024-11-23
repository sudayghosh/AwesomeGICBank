using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeGIC.Bank.Domain.Common
{
    public abstract class KeyedEntity : AuditableEntity
    {
        public Guid Id { get; set; }
    }
}
