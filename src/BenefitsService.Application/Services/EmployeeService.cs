using BenefitsService.Application.DTO;
using BenefitsService.Application.Interfaces;
using BenefitsService.Domain.Aggregates;
using BenefitsService.Domain.Interfaces;
using System.Net;
using BenefitsService.Application.Extensions;
using BenefitsService.Domain.Enums;

namespace BenefitsService.Application.Services
{
    /// <summary>
    /// Provides operations for managing employees and their dependents, including retrieval, updates, and additions.
    /// </summary>
    /// <remarks>This service interacts with an employee aggregate repository to perform operations such as
    /// fetching employee data, updating employee information, and adding dependents. It returns API responses that
    /// encapsulate the results of these operations.</remarks>
    /// <param name="_employeeAggregateRepository">Domain interface for data access operations of the core
    /// domain entities.
    /// </param>
    public class EmployeeService(IEmployeeAggregateRepository _employeeAggregateRepository) : IEmployeeService
    {
        private const int DefaultPageSize = 10;
        private const int DefaultOffset = 0;

        /// <summary>
        /// Gets an employee by their ID.
        /// </summary>
        /// <param name="id">ID of the employee.</param>
        /// <returns>
        /// An API response object containing an employee DTO with other metadata such as HTTP response code.
        /// </returns>
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

        /// <summary>
        /// Gets a paginated list of employees.
        /// </summary>
        /// <param name="pageSize">Number of records to get in a specific query.</param>
        /// <param name="offset">The zero-based index of the first entity to retrieve. Must be non-negative.</param>
        /// <returns>
        /// An API response object containing an enumerable collection of employee DTOs with other
        /// metadata such as total count and HTTP response code.
        /// </returns>
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

        /// <summary>
        /// Updates an existing employee's information.
        /// </summary>
        /// <param name="id">ID of the employee.</param>
        /// <param name="employee">Updated information for the employee.</param>
        /// <returns>
        /// An API response object containing an employee DTO with other information detailing if the
        /// addition was successful, and why not if it was not.
        /// </returns>
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

        /// <summary>
        /// Adds a new dependent to an employee's record.
        /// </summary>
        /// <param name="employeeId">ID of the employee.</param>
        /// <param name="dependent">Information about the new dependent.</param>
        /// <returns>
        /// An API response object containing an employee DTO with other information detailing if the
        /// addition was successful, and why not if it was not.
        /// </returns>
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
