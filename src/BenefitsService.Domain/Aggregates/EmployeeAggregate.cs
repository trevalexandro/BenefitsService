using BenefitsService.Domain.Entities;
using BenefitsService.Domain.Enums;
using BenefitsService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenefitsService.Domain.Aggregates
{
    public class EmployeeAggregate : RootEntity
    {
        public const string SpouseDependentName = "Spouse";
        public const string DomesticPartnerDependentName = "Domestic Partner";
        public const int BaseBenefitsDeduction = 1000;
        public const int BaseDependentDeduction = 600;
        public const int OlderDependentDeductionMinimumAge = 50;
        public const int OlderDependentDeduction = 200;
        public const int HighEarnerDeductionMinimumSalary = 80000;
        public const decimal HighEarnerDeductionPercentage = 0.02M;

        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required DateOnly DateOfBirth { get; set; }
        public int AnnualGrossSalary { get; set; }
        public ICollection<Dependent> Dependents { get; set; } = [];

        public override (bool Valid, string Error) ValidateEntity()
        {
            var partnerDependents = Dependents.Where(dependent => Enum.GetName(dependent.Relationship) == 
                SpouseDependentName || Enum.GetName(dependent.Relationship) == DomesticPartnerDependentName);
            if (partnerDependents.Count() > 1)
            {
                return (false, "Employees can only have one spouse OR one domestic partner");
            }

            return base.ValidateEntity();
        }

        public decimal CalculateNetSalary()
        {
            decimal deductionsTotal = BaseBenefitsDeduction * 12;
            if (AnnualGrossSalary > HighEarnerDeductionMinimumSalary)
            {
                decimal highEarnerDeduction = (AnnualGrossSalary * HighEarnerDeductionPercentage);
                deductionsTotal += highEarnerDeduction;
            }

            foreach (var dependent in Dependents)
            {
                int dependentsDeductions = BaseDependentDeduction * 12;
                if (dependent.Age > OlderDependentDeductionMinimumAge)
                {
                    dependentsDeductions += OlderDependentDeduction * 12;
                }

                deductionsTotal += dependentsDeductions;
            }

            decimal netPaycheck = AnnualGrossSalary - deductionsTotal;
            return netPaycheck;
        }
    }
}
