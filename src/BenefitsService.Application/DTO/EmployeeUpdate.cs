
namespace BenefitsService.Application.DTO
{
    public class EmployeeUpdate
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public int? AnnualGrossSalary { get; set; }
    }
}
