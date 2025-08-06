
namespace BenefitsService.Domain.Entities
{
    /// <summary>
    /// Base entity class that all entities inherit from.
    /// </summary>
    public abstract class BaseEntity
    {
        public required Guid Id { get; set; }
    }
}
