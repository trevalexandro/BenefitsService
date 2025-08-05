using BenefitsService.Domain.Entities;
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
        public const int BaseBenefitsDeduction = 500;
        public const int BaseDependentDeduction = 300;
        public const int OlderDependentDeductionMinimumAge = 50;
        public const int OlderDependentDeduction = 200;
        public const int HighEarnerDeductionMinimumSalary = 80000;
        public const double HighEarnerDeductionPercentage = 0.02;

        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required DateOnly DateOfBirth { get; set; }
        public int AnnualGrossSalary { get; set; }
        public ICollection<Dependent> Dependents { get; set; } = [];

        public override bool ValidateEntity()
        {
            var partnerDependents = Dependents.Where(dependent => dependent.Relationship.Name == SpouseDependentName ||
                dependent.Relationship.Name == DomesticPartnerDependentName);
            if (partnerDependents.Count() > 1)
            {
                return false; // Only one spouse or domestic partner is allowed
            }

            return base.ValidateEntity();
        }

        public double CalculateNetPaycheck()
        {
            double deductionsTotal = BaseBenefitsDeduction;

            if (AnnualGrossSalary > HighEarnerDeductionMinimumSalary)
            {
                deductionsTotal += (AnnualGrossSalary * HighEarnerDeductionPercentage); // Additional deductions for high earners
            }

            foreach (var dependent in Dependents)
            {
                int dependentsDeductions = BaseDependentDeduction;
                if (dependent.Age > OlderDependentDeductionMinimumAge)
                {
                    dependentsDeductions += OlderDependentDeduction;
                }

                deductionsTotal += dependentsDeductions;
            }

            double biweeklySalary = AnnualGrossSalary / 26;
            double netPaycheck = biweeklySalary - deductionsTotal;
            return netPaycheck;
        }
    }
}
