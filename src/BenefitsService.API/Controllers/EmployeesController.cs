using BenefitsService.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BenefitsService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController(IEmployeeService _employeeService) : ControllerBase
    {
        public IActionResult Get()
        {
            var response = _employeeService.GetEmployeesAsync();
            return Ok(response);
        }
    }
}
