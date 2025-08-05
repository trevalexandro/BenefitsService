using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenefitsService.Domain.Entities
{
    public abstract class ChildEntity<TAggregate> : BaseEntity where TAggregate : BaseEntity
    {
        public required TAggregate Parent { get; set; }
    }
}
