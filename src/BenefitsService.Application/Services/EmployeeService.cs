using BenefitsService.Application.DTO;
using BenefitsService.Application.Interfaces;
using BenefitsService.Domain.Aggregates;
using Entities = BenefitsService.Domain.Entities;
using BenefitsService.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BenefitsService.Application.Extensions;
using BenefitsService.Domain.Enums;

namespace BenefitsService.Application.Services
{
    public class EmployeeService(IEmployeeAggregateRepository _employeeAggregateRepository) : IEmployeeService
    {
        private const int DefaultPageSize = 10;
        private const int DefaultOffset = 0;

        public async Task<ApiResponse<Employee>> GetEmployeeByIdAsync(Guid id)
        {
            var employeeEntity = await _employeeAggregateRepository.GetEmployeeWithDependentsAsync(id);
            if (employeeEntity == null)
            {
                return new NotFoundApiResponse<Employee>();
            }
            
            var employee = employeeEntity.ToFullDto();
            var response = new ApiResponse<Employee>
            {
                Data = employee
            };

            return response;
        }

        public async Task<ApiResponse<IEnumerable<Employee>>> GetEmployeesAsync(int? pageSize, int? offset)
        {
            int totalCount = await _employeeAggregateRepository.CountAsync<EmployeeAggregate>();
            var employeeEntities = await _employeeAggregateRepository.GetAllAsync<EmployeeAggregate>(
                pageSize ?? DefaultPageSize, offset ?? DefaultOffset);
            var employeeDtos = employeeEntities.Select(entity => entity.ToDtoWithoutNetPay());
            var response = new ApiResponse<IEnumerable<Employee>>
            {
                Data = employeeDtos,
                TotalCount = totalCount
            };
            return response;
        }

        public async Task<ApiResponse<Employee>> UpdateEmployeeAsync(Guid id, EmployeeUpdate employee)
        {
            var employeeEntity = await _employeeAggregateRepository.GetEmployeeWithDependentsAsync(id);
            if (employeeEntity == null)
            {
                return new NotFoundApiResponse<Employee>();
            }

            var updatedEmployeeEntity = employee.ToEntity(employeeEntity);
            await _employeeAggregateRepository.SaveChangesAsync(updatedEmployeeEntity);
            var employeeDto = updatedEmployeeEntity.ToDtoWithoutNetPay();
            return new ApiResponse<Employee>
            {
                Data = employeeDto
            };
        }

        public async Task<ApiResponse<Employee>> AddDependentAsync(Guid employeeId, NewDependent dependent)
        {
            var employeeEntity = await _employeeAggregateRepository.GetEmployeeWithDependentsAsync(employeeId);
            if (employeeEntity == null)
            {
                return new NotFoundApiResponse<Employee>();
            }

            dependent.Relationship = dependent.Relationship.Replace(" ", string.Empty);
            if (!Enum.TryParse<Relationship>(dependent.Relationship, out var relationship))
            {
                return new BadRequestApiResponse<Employee>
                {
                    Error = "Invalid relationship for new dependent"
                };
            }

            var dependentEntity = dependent.ToEntity(employeeEntity, relationship);
            employeeEntity.Dependents.Add(dependentEntity);
            (bool Valid, string Error) = employeeEntity.ValidateEntity();
            if (!Valid)
            {
                return new BadRequestApiResponse<Employee>
                {
                    Error = Error
                };
            }

            await _employeeAggregateRepository.SaveChangesAsync(employeeEntity);
            var result = employeeEntity.ToFullDto();
            return new ApiResponse<Employee>
            {
                Data = result,
                StatusCode = HttpStatusCode.Created
            };
        }
    }
}
