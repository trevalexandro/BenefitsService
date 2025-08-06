
namespace BenefitsService.Domain.Entities
{
    /// <summary>
    /// Represents the base class for all entities that are either an aggregate root or can be mutated directly.
    /// </summary>
    public abstract class RootEntity : BaseEntity
    {
        /// <summary>
        /// Validates whether the current entity can be inserted or updated in the database.
        /// </summary>
        /// <returns>
        /// Tuple containing a boolean value of whether the entity was successfully validated or not, and
        /// a string containing an error message if validation failed.
        /// </returns>
        public virtual (bool Valid, string Error) ValidateEntity()
        {
            return (true, string.Empty);
        }
    }
}
