using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenefitsService.Domain.Entities
{
    public class DependentRelationship : RootEntity
    {
        public required string Name { get; set; }
    }
}
