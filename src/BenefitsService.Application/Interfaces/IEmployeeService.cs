using BenefitsService.Application.DTO;

namespace BenefitsService.Application.Interfaces
{
    /// <summary>
    /// Interface for employee-related operations in the application layer.
    /// </summary>
    public interface IEmployeeService
    {
        /// <summary>
        /// Gets a paginated list of employees.
        /// </summary>
        /// <param name="pageSize">Number of records to get in a specific query.</param>
        /// <param name="offset">The zero-based index of the first entity to retrieve. Must be non-negative.</param>
        /// <returns>
        /// An API response object containing an enumerable collection of employee DTOs with other
        /// metadata such as total count and HTTP response code.
        /// </returns>
        Task<ApiResponse<IEnumerable<Employee>>> GetEmployeesAsync(int? pageSize, int? offset);

        /// <summary>
        /// Gets an employee by their ID.
        /// </summary>
        /// <param name="id">ID of the employee.</param>
        /// <returns>
        /// An API response object containing an employee DTO with other metadata such as HTTP response code.
        /// </returns>
        Task<ApiResponse<Employee>> GetEmployeeByIdAsync(Guid id);

        /// <summary>
        /// Adds a new dependent to an employee's record.
        /// </summary>
        /// <param name="employeeId">ID of the employee.</param>
        /// <param name="dependent">Information about the new dependent.</param>
        /// <returns>
        /// An API response object containing an employee DTO with other information detailing if the
        /// addition was successful, and why not if it was not.
        /// </returns>
        Task<ApiResponse<Employee>> AddDependentAsync(Guid employeeId, NewDependent dependent);

        /// <summary>
        /// Updates an existing employee's information.
        /// </summary>
        /// <param name="id">ID of the employee.</param>
        /// <param name="employee">Updated information for the employee.</param>
        /// <returns>
        /// An API response object containing an employee DTO with other information detailing if the
        /// addition was successful, and why not if it was not.
        /// </returns>
        Task<ApiResponse<Employee>> UpdateEmployeeAsync(Guid id, EmployeeUpdate employee);
    }
}
