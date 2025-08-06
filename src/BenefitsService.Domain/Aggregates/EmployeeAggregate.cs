using BenefitsService.Domain.Entities;
using BenefitsService.Domain.Enums;

namespace BenefitsService.Domain.Aggregates
{
    /// <summary>
    /// Aggregate representing an employee in the benefits system.
    /// </summary>
    public class EmployeeAggregate : RootEntity
    {
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

        /// <summary>
        /// Validates whether the current entity can be inserted or updated in the database.
        /// </summary>
        /// <returns>
        /// Tuple containing a boolean value of whether the entity was successfully validated or not, and
        /// a string containing an error message if validation failed.
        /// </returns>
        public override (bool Valid, string Error) ValidateEntity()
        {
            var partnerDependents = Dependents.Where(dependent => dependent.Relationship == Relationship.Spouse || 
                dependent.Relationship == Relationship.DomesticPartner);
            if (partnerDependents.Count() > 1)
            {
                return (false, "Employees can only have one spouse OR one domestic partner");
            }

            return base.ValidateEntity();
        }

        /// <summary>
        /// Calculates the net annual salary after applying all applicable deductions.
        /// </summary>
        /// <remarks>This method computes the net salary by subtracting various deductions from the annual
        /// gross salary. Deductions include base benefits, high earner deductions (if applicable), and
        /// dependent-related deductions. Dependent deductions may vary based on the age of each dependent.</remarks>
        /// <returns>
        /// The net annual salary as a <see cref="decimal"/> value after all deductions have been applied.
        /// </returns>
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
