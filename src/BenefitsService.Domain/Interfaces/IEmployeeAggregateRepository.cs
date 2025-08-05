using BenefitsService.Domain.Aggregates;
using BenefitsService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenefitsService.Domain.Interfaces
{
    public interface IEmployeeAggregateRepository : IDataRepository
    {
        Task<EmployeeAggregate?> GetEmployeeWithDependentsAsync(Guid employeeId);
    }
}
