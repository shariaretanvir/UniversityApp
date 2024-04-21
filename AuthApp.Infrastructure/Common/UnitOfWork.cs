using AuthApp.Core.Infra;
using AuthApp.Domain.Common;
using AuthApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthApp.Infrastructure.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AuthAppDbContext _dbcontext;
        private Dictionary<Type, object> _dictionary;
        public UnitOfWork(AuthAppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : Entity<TKey>
        {
            Type type = typeof(TEntity);
            _dictionary ??= new Dictionary<Type, object>();
            if (!_dictionary.ContainsKey(type))
                _dictionary.Add(type, new GenericRepository<TEntity, TKey>(_dbcontext));

            return (IGenericRepository<TEntity, TKey>)_dictionary[type];
        }

        public async Task<int> CommitAsync(CancellationToken token = default)
        {
            return await _dbcontext.SaveChangesAsync(token);
        }
    }
}
