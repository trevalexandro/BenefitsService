using BenefitsService.Application.DTO;
using BenefitsService.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BenefitsService.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController(IEmployeeService _employeeService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAsync(int? pageSize, int? offset)
        {
            try
            {
                var response = await _employeeService.GetEmployeesAsync(pageSize, offset);
                return Ok(response);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, 
                    new InternalServerErrorApiResponse<IEnumerable<Employee>>());
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            try
            {
                var response = await _employeeService.GetEmployeeByIdAsync(id);
                return Ok(response);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, 
                    new InternalServerErrorApiResponse<Employee>());
            }
        }

        [HttpPost("{id:guid}/dependents")]
        public async Task<IActionResult> PostAsync(Guid id, [FromBody] Dependent dependent)
        {
            try
            {
                var response = await _employeeService.AddDependentAsync(id, dependent);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, 
                    new InternalServerErrorApiResponse<Employee>());
            }
        }
    }
}
