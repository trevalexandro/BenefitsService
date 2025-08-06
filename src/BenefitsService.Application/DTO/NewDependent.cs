
namespace BenefitsService.Application.DTO
{
    public class NewDependent
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required int Age { get; set; }
        public required string Relationship { get; set; }
    }
}
