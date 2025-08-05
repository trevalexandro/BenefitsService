using BenefitsService.Domain.Entities;
using BenefitsService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BenefitsService.Infrastructure.Repositories
{
    public class EFDataRepository(BenefitsServiceContext dbContext) : IDataRepository
    {
        protected readonly BenefitsServiceContext _dbContext = dbContext;

        public async Task<int> CountAsync<TEntity>() where TEntity : BaseEntity
        {
            int count = await _dbContext.Set<TEntity>().AsNoTracking().CountAsync();
            return count;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(int pageSize, int offset) where TEntity : BaseEntity
        {
            var queryable = _dbContext.Set<TEntity>().AsNoTracking().AsQueryable();
            var results = await queryable.Skip(offset).Take(pageSize).ToListAsync();
            return results;
        }

        public async Task<TEntity?> GetAsync<TEntity>(Guid id) where TEntity : BaseEntity
        {
            var entity = await _dbContext.Set<TEntity>().FindAsync(id);
            return entity;
        }

        public async Task<Guid?> SaveChangesAsync<TEntity>(TEntity entity) where TEntity : RootEntity
        {
            if (!ValidateEntity(entity))
            {
                return null;
            }

            var dbSet = _dbContext.Set<TEntity>();
            if (entity.Id == null)
            {
                var result = await dbSet.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                return result.Entity.Id;
            }

            var existingEntity = await dbSet.FindAsync(entity.Id);
            if (existingEntity == null)
            {
                return null;
            }

            _dbContext.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _dbContext.SaveChangesAsync();
            return existingEntity.Id;
        }

        public bool ValidateEntity<TEntity>(TEntity entity) where TEntity : RootEntity
        {
            return entity.ValidateEntity();
        }
    }
}
