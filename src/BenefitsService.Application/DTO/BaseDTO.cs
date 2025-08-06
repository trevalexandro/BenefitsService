using System.Text.Json.Serialization;

namespace BenefitsService.Application.DTO
{
    public abstract class BaseDTO
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public Guid Id { get; set; }
    }
}
