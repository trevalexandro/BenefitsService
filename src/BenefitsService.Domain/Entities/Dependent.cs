using BenefitsService.Domain.Aggregates;
using BenefitsService.Domain.Enums;

namespace BenefitsService.Domain.Entities
{
    /// <summary>
    /// Represents a dependent associated with an employee, including personal details and their relationship to the
    /// employee.
    /// </summary>
    /// <remarks>This class is used to model dependents in the context of an employee aggregate. Each
    /// dependent has required properties  such as their name, age, and relationship to the employee.</remarks>
    public class Dependent : ChildEntity<EmployeeAggregate>
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required int Age { get; set; }
        public required Relationship Relationship { get; set; }
    }
}
