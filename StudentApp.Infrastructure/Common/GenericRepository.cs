using Microsoft.EntityFrameworkCore;
using StudentApp.Core.Infra;
using StudentApp.Domain.Common;
using StudentApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Infrastructure.Common
{
    public class GenericRepository<T, TKey> : IGenericRepository<T, TKey> where T : Entity<TKey>
    {
        protected readonly StudentDbContext _dbContext;

        public GenericRepository(StudentDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
        }

        public async Task AddRangeAsync(List<T> entities)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public void DeleteRange(List<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(TKey id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public IQueryable<T> Query(params Expression<Func<T, object>>[] expressions)
        {
            IQueryable<T> query = _dbContext.Set<T>();
            
            if(expressions != null)
            {
                foreach (var expression in expressions)
                {
                    query = query.Include(expression);
                }
            }
            return query;
        }

        public void Update(T entity)
        {
            _dbContext.Entry<T>(entity).State = EntityState.Modified;
            _dbContext.Set<T>().Update(entity);
        }

        public void UpdateRange(List<T> entities)
        {
            _dbContext.Set<T>().UpdateRange(entities);
        }
    }
}
