using StudentApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Core.Infra
{
    public interface IGenericRepository<T, TKey> where T : Entity<TKey>
    {
        IQueryable<T> Query(params Expression<Func<T, object>>[] expressions);
        Task<T?> GetByIdAsync(TKey id);
        Task<List<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task AddRangeAsync(List<T> entities);
        void  Update(T entity);
        void UpdateRange(List<T> entities);
        void Delete(T entity);
        void DeleteRange(List<T> entities);
    }
}
