using BenefitsService.Domain.Aggregates;
using BenefitsService.Domain.Enums;
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
        public required int Age { get; set; }
        public required Relationship Relationship { get; set; }
    }
}
