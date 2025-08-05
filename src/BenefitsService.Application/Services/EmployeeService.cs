using BenefitsService.Application.DTO;
using BenefitsService.Application.Interfaces;
using BenefitsService.Domain.Aggregates;
using Entities = BenefitsService.Domain.Entities;
using BenefitsService.Domain.Interfaces;
using Mapster;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BenefitsService.Application.Services
{
    public class EmployeeService(IEmployeeAggregateRepository _dataRepository) : IEmployeeService
    {
        public async Task<ApiResponse<Employee>> GetEmployeeByIdAsync(Guid id)
        {
            var employeeEntity = await _dataRepository.GetEmployeeWithDependentsAsync(id);
            if (employeeEntity == null)
            {
                return new NotFoundApiResponse<Employee>();
            }
            
            var employee = employeeEntity.Adapt<Employee>();
            employee.Dependents = [.. employeeEntity.Dependents.Select(dependent => dependent.Adapt<Dependent>())];
            var response = new ApiResponse<Employee>
            {
                Data = employee
            };

            return response;
        }

        public async Task<ApiResponse<IEnumerable<Employee>>> GetEmployeesAsync(int pageSize = 10, int offset = 0)
        {
            int totalCount = await _dataRepository.CountAsync<EmployeeAggregate>();
            var employeeEntities = await _dataRepository.GetAllAsync<EmployeeAggregate>(pageSize, offset);
            var employeeDtos = employeeEntities.Select(entity => entity.Adapt<Employee>());
            var response = new ApiResponse<IEnumerable<Employee>>
            {
                Data = employeeDtos,
                TotalCount = totalCount
            };
            return response;
        }

        public async Task<ApiResponse<Employee>> UpdateEmployeeAsync(Employee employee)
        {
            var employeeEntity = await _dataRepository.GetAsync<EmployeeAggregate>(employee.Id.GetValueOrDefault());
            if (employeeEntity == null)
            {
                return new NotFoundApiResponse<Employee>();
            }

            employeeEntity.FirstName = employee.FirstName;
            employeeEntity.LastName = employee.LastName;
            employeeEntity.DateOfBirth = employee.DateOfBirth;

            await _dataRepository.SaveChangesAsync(employeeEntity);
            return new ApiResponse<Employee>
            {
                Data = employee
            };
        }
    }
}
