using AuthApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AuthApp.Core.Infra
{
    public interface IGenericRepository<TEntity, Tkey> where TEntity : Entity<Tkey>
    {
        IQueryable<TEntity> Query(params Expression<Func<TEntity, object>>[] expressions);
        Task<TEntity?> GetByIdAsync(Tkey id);
        Task<List<TEntity>> GetAllAsync();
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(List<TEntity> entities);
        void Update(TEntity entity);
        void UpdateRange(List<TEntity> entities);
        void Delete(TEntity entity);
        void DeleteRange(List<TEntity> entities);

    }
}
