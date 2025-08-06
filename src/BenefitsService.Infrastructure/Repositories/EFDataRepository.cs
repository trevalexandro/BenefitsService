using BenefitsService.Domain.Entities;
using BenefitsService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BenefitsService.Infrastructure.Repositories
{
    /// <summary>
    /// Entity Framework Core implementation of the core data repository interface.
    /// </summary>
    /// <param name="dbContext">Object used for interacting with the database.</param>
    public class EFDataRepository(BenefitsServiceContext dbContext) : IDataRepository
    {
        protected readonly BenefitsServiceContext _dbContext = dbContext;

        /// <summary>
        /// Asynchronously counts the total number of entities of the specified type in the data source.
        /// </summary>
        /// <remarks>This method queries the data source to determine the count of entities of the
        /// specified type. It can be used in conjunction with pagination to get the total count.</remarks>
        /// <typeparam name="TEntity">The type of entity to count.</typeparam>
        /// <returns>The total count of entities.</returns>
        public async Task<int> CountAsync<TEntity>() where TEntity : BaseEntity
        {
            int count = await _dbContext.Set<TEntity>().AsNoTracking().CountAsync();
            return count;
        }

        /// <summary>
        /// Retrieves a paginated collection of all entities of the specified type.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity to retrieve.</typeparam>
        /// <param name="pageSize">The maximum number of entities to include in the result.</param>
        /// <param name="offset">The zero-based index of the first entity to retrieve. Must be non-negative.</param>
        /// <returns>An enumerable collection of <typeparamref name="TEntity"/> objects.</returns>
        public async Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(int pageSize, int offset) where TEntity : BaseEntity
        {
            var queryable = _dbContext.Set<TEntity>().AsNoTracking().AsQueryable();
            var results = await queryable.Skip(offset).Take(pageSize).ToListAsync();
            return results;
        }

        /// <summary>
        /// Retrieve an entity by its ID.
        /// </summary>
        /// <typeparam name="TEntity">Type of domain entity.</typeparam>
        /// <param name="id">ID of the entity.</param>
        /// <returns>Entity with the matching ID, or null if no entity with the provided ID exists.</returns>
        public async Task<TEntity?> GetAsync<TEntity>(Guid id) where TEntity : BaseEntity
        {
            var entity = await _dbContext.Set<TEntity>().FindAsync(id);
            return entity;
        }

        /// <summary>
        /// Insert or update an entity in the data store.
        /// </summary>
        /// <typeparam name="TEntity">Type of domain entity.</typeparam>
        /// <param name="entity">Entity being inserted or updated.</param>
        /// <returns>ID of the entity.</returns>
        public async Task<Guid?> SaveChangesAsync<TEntity>(TEntity entity) where TEntity : RootEntity
        {
            if (!ValidateEntity(entity).Valid)
            {
                return null;
            }

            var dbSet = _dbContext.Set<TEntity>();
            if (entity.Id == default)
            {
                var result = await dbSet.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                return result.Entity.Id;
            }

            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return entity.Id;
        }

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
        public (bool Valid, string Error) ValidateEntity<TEntity>(TEntity entity) where TEntity : RootEntity
        {
            return entity.ValidateEntity();
        }
    }
}
