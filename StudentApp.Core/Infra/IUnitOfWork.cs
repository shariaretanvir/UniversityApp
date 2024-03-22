using StudentApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Core.Infra
{
    public interface IUnitOfWork
    {
        IGenericRepository<T, TKey> GetRepository<T, TKey>() where T : Entity<TKey>;
        Task<int> CommitAsync(CancellationToken token = default);
    }
}
