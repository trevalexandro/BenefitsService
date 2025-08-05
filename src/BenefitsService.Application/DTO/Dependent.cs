using BenefitsService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities = BenefitsService.Domain.Entities;
using Aggregates = BenefitsService.Domain.Aggregates;

namespace BenefitsService.Application.DTO
{
    public class Dependent : BaseDTO
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public int Age { get; set; }
        public required string Relationship { get; set; }
    }
}
