using BenefitsService.Application.DTO;
using BenefitsService.Domain.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenefitsService.Application.Interfaces
{
    public interface IEmployeeService
    {
        Task<ApiResponse<IEnumerable<Employee>>> GetEmployeesAsync(int? pageSize, int? offset);
        Task<ApiResponse<Employee>> GetEmployeeByIdAsync(Guid id);
        Task<ApiResponse<Employee>> AddDependentAsync(Guid employeeId, NewDependent dependent);
    }
}
