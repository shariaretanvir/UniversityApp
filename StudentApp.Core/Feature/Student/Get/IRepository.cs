using StudentApp.Core.Common;
using StudentApp.Core.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Core.Feature.Student.Get
{
    public interface IRepository : IGenericRepository<Entities.Student, Guid>
    {
        Task<PagedList<Entities.Student>> GetAll(ResourceParameters resourceParameters); 
    }
}
