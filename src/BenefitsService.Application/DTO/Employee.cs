using BenefitsService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aggregates = BenefitsService.Domain.Aggregates;

namespace BenefitsService.Application.DTO
{
    public class Employee : BaseDTO
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required DateOnly DateOfBirth { get; set; }
        public int AnnualGrossSalary { get; set; }
        public decimal NetSalary { get; set; }
        public decimal NetMonthlyPay { get; set; }
        public decimal NetBiweeklyPay { get; set; }
        public IEnumerable<Dependent>? Dependents { get; set; }

    }
}
