using BenefitsService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BenefitsService.Domain.Interfaces
{
    public interface IDataRepository
    {
        Task<Guid?> SaveChangesAsync<TEntity>(TEntity entity) where TEntity : RootEntity;
        Task<TEntity?> GetAsync<TEntity>(Guid id) where TEntity : BaseEntity;
        Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(int pageSize, int offset) where TEntity : BaseEntity;
        Task<int> CountAsync<TEntity>() where TEntity : BaseEntity;
        bool ValidateEntity<TEntity>(TEntity entity) where TEntity : RootEntity;
    }
}
