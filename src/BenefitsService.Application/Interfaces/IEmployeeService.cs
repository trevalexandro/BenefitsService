using BenefitsService.Application.DTO;
using BenefitsService.Domain.Aggregates;
using BenefitsService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenefitsService.Application.Interfaces
{
    public interface IEmployeeService
    {
        private const int DefaultPageSize = 10;
        private const int DefaultOffset = 0;

        Task<ApiResponse<IEnumerable<Employee>>> GetEmployeesAsync(int pageSize = DefaultPageSize, int offset = DefaultOffset);
        Task<ApiResponse<Employee>> GetEmployeeByIdAsync(Guid id);
        Task<ApiResponse<Employee>> UpdateEmployeeAsync(Employee employee);
    }
}
