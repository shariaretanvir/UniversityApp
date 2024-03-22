using StudentApp.Core.Infra;
using StudentApp.Domain.Common;
using StudentApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Infrastructure.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StudentDbContext _dbContext;
        private Dictionary<Type, object> _repositories;
        public UnitOfWork(StudentDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> CommitAsync(CancellationToken token = default)
        {
            return await _dbContext.SaveChangesAsync(token);
        }

        public IGenericRepository<T, TKey> GetRepository<T, TKey>() where T : Entity<TKey>
        {
            _repositories ??= new Dictionary<Type, object>();
            Type type = typeof(T);
            if(!_repositories.ContainsKey(type))
                _repositories.Add(type, new GenericRepository<T, TKey>(_dbContext));
            return (IGenericRepository<T, TKey>)_repositories[type];
            
        }
    }
}
