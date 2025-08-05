using BenefitsService.Domain.Aggregates;
using BenefitsService.Domain.Entities;
using BenefitsService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenefitsService.Infrastructure.Repositories
{
    public class EmployeeAggregateRepository(BenefitsServiceContext dbContext) : EFDataRepository(dbContext), 
        IEmployeeAggregateRepository
    {
        public async Task<EmployeeAggregate?> GetEmployeeWithDependentsAsync(Guid employeeId)
        {
            var employee = await _dbContext.Employees
                .Include(employee => employee.Dependents)
                .ThenInclude(dependent => dependent.Relationship)
                .FirstOrDefaultAsync(employee => employee.Id == employeeId);

            return employee;
        }
    }
}
