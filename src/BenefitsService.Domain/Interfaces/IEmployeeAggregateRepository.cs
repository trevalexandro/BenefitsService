using BenefitsService.Domain.Aggregates;

namespace BenefitsService.Domain.Interfaces
{
    /// <summary>
    /// Domain interface that adds additional methods for managing employee aggregates on top of the core data 
    /// repository interface.
    /// </summary>
    public interface IEmployeeAggregateRepository : IDataRepository
    {
        /// <summary>
        /// Retrieves an employee along with their dependents.
        /// </summary>
        /// <param name="employeeId">The unique identifier of the employee to retrieve.</param>
        /// <returns>An  <see cref="EmployeeAggregate"/> object representing the employee and their dependents, 
        /// or <see langword="null"/> if no employee with the specified ID exists.</returns>
        Task<EmployeeAggregate?> GetEmployeeWithDependentsAsync(Guid employeeId);
    }
}
