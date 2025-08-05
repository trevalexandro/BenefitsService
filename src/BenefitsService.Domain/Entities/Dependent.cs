using BenefitsService.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenefitsService.Domain.Entities
{
    public class Dependent : ChildEntity<EmployeeAggregate>
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public int Age { get; set; }
        public required DependentRelationship Relationship { get; set; }
    }
}
