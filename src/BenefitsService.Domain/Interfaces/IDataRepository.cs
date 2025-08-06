using BenefitsService.Domain.Entities;

namespace BenefitsService.Domain.Interfaces
{
    /// <summary>
    /// Core interface for data access operations of the Benefits Service domain.
    /// </summary>
    public interface IDataRepository
    {
        /// <summary>
        /// Insert or update an entity in the data store.
        /// </summary>
        /// <typeparam name="TEntity">Type of domain entity.</typeparam>
        /// <param name="entity">Entity being inserted or updated.</param>
        /// <returns>ID of the entity.</returns>
        Task<Guid?> SaveChangesAsync<TEntity>(TEntity entity) where TEntity : RootEntity;

        /// <summary>
        /// Retrieve an entity by its ID.
        /// </summary>
        /// <typeparam name="TEntity">Type of domain entity.</typeparam>
        /// <param name="id">ID of the entity.</param>
        /// <returns>Entity with the matching ID, or null if no entity with the provided ID exists.</returns>
        Task<TEntity?> GetAsync<TEntity>(Guid id) where TEntity : BaseEntity;

        /// <summary>
        /// Retrieves a paginated collection of all entities of the specified type.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity to retrieve.</typeparam>
        /// <param name="pageSize">The maximum number of entities to include in the result.</param>
        /// <param name="offset">The zero-based index of the first entity to retrieve. Must be non-negative.</param>
        /// <returns>An enumerable collection of <typeparamref name="TEntity"/> objects.</returns>
        Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(int pageSize, int offset) where TEntity : BaseEntity;

        /// <summary>
        /// Asynchronously counts the total number of entities of the specified type in the data source.
        /// </summary>
        /// <remarks>This method queries the data source to determine the count of entities of the
        /// specified type. It can be used in conjunction with pagination to get the total count.</remarks>
        /// <typeparam name="TEntity">The type of entity to count.</typeparam>
        /// <returns>The total count of entities.</returns>
        Task<int> CountAsync<TEntity>() where TEntity : BaseEntity;

        /// <summary>
        /// Validates the specified entity to ensure it meets the requirements for insertion or update.
        /// This enforces consumers of the domain interface to add validation logic to their implmementations.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity to validate.</typeparam>
        /// <param name="entity">The entity being validated.</param>
        /// <returns>
        /// Tuple containing a boolean value of whether the entity was successfully validated or not, and
        /// a string containing an error message if validation failed.
        /// </returns>
        (bool Valid, string Error) ValidateEntity<TEntity>(TEntity entity) where TEntity : RootEntity;
    }
}
