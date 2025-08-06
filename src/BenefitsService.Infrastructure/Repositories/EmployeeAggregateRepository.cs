using BenefitsService.Domain.Aggregates;
using BenefitsService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BenefitsService.Infrastructure.Repositories
{
    /// <summary>
    /// Entity Framework Core implementation of the employee aggregate repository interface.
    /// </summary>
    /// <param name="dbContext">Object used for interacting with the database.</param>
    public class EmployeeAggregateRepository(BenefitsServiceContext dbContext) : EFDataRepository(dbContext), 
        IEmployeeAggregateRepository
    {
        /// <summary>
        /// Retrieves an employee along with their dependents.
        /// </summary>
        /// <param name="employeeId">The unique identifier of the employee to retrieve.</param>
        /// <returns>An  <see cref="EmployeeAggregate"/> object representing the employee and their dependents, 
        /// or <see langword="null"/> if no employee with the specified ID exists.</returns>
        public async Task<EmployeeAggregate?> GetEmployeeWithDependentsAsync(Guid employeeId)
        {
            var result = await _dbContext.Employees
                .Include(employee => employee.Dependents)
                .FirstOrDefaultAsync(employee => employee.Id == employeeId);

            return result;
        }
    }
}
