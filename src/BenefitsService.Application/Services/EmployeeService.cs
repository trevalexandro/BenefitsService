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
using BenefitsService.Application.Extensions;
using BenefitsService.Domain.Enums;

namespace BenefitsService.Application.Services
{
    public class EmployeeService(IEmployeeAggregateRepository _dataRepository) : IEmployeeService
    {
        private const int DefaultPageSize = 10;
        private const int DefaultOffset = 0;

        public async Task<ApiResponse<Employee>> GetEmployeeByIdAsync(Guid id)
        {
            var employeeEntity = await _dataRepository.GetEmployeeWithDependentsAsync(id);
            if (employeeEntity == null)
            {
                return new NotFoundApiResponse<Employee>();
            }
            
            var employee = employeeEntity.ToFullDto();
            var dependents = employeeEntity.Dependents.ToList().Select(dependent => dependent.ToDto());
            employee.Dependents = [.. dependents];
            var response = new ApiResponse<Employee>
            {
                Data = employee
            };

            return response;
        }

        public async Task<ApiResponse<IEnumerable<Employee>>> GetEmployeesAsync(int? pageSize, int? offset)
        {
            int totalCount = await _dataRepository.CountAsync<EmployeeAggregate>();
            var employeeEntities = await _dataRepository.GetAllAsync<EmployeeAggregate>(pageSize ?? DefaultPageSize,
                offset ?? DefaultOffset);
            var employeeDtos = employeeEntities.Select(entity => entity.ToDtoWithoutNetPay());
            var response = new ApiResponse<IEnumerable<Employee>>
            {
                Data = employeeDtos,
                TotalCount = totalCount
            };
            return response;
        }

        public async Task<ApiResponse<Employee>> AddDependentAsync(Guid employeeId, Dependent dependent)
        {
            var employeeEntity = await _dataRepository.GetEmployeeWithDependentsAsync(employeeId);
            if (employeeEntity == null)
            {
                return new NotFoundApiResponse<Employee>();
            }

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

            await _dataRepository.SaveChangesAsync(employeeEntity);
            var result = employeeEntity.ToFullDto();
            return new ApiResponse<Employee>
            {
                Data = result,
                Success = true,
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}
