
namespace BenefitsService.Domain.Entities
{
    /// <summary>
    /// Represents a child entity that is associated with a parent aggregate entity.
    /// </summary>
    /// <typeparam name="TAggregate">Type of parent aggregate entity.</typeparam>
    public abstract class ChildEntity<TAggregate> : BaseEntity where TAggregate : BaseEntity
    {
        public required TAggregate Parent { get; set; }
    }
}
