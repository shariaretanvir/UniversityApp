using Microsoft.EntityFrameworkCore;
using StudentApp.Core.Common;
using StudentApp.Core.Feature.Student.Get;
using StudentApp.Domain.Entities;
using StudentApp.Infrastructure.Common;
using StudentApp.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Infrastructure.Repository
{
    public class StudentRepository : GenericRepository<Student, Guid>, IRepository
    {
        private readonly StudentDbContext _dbContext;

        public StudentRepository(StudentDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<PagedList<Student>> GetAll(ResourceParameters resourceParameters)
        {
            IQueryable<Student> query = _dbContext.Set<Student>()
                .Include(x=>x.StudentAddresses);

            if (!string.IsNullOrWhiteSpace(resourceParameters.FieldName))
            {
                var type = typeof(Student).GetProperty(resourceParameters.FieldName, BindingFlags.IgnoreCase);
                query = resourceParameters.OrderBy == "asc" ? query.OrderBy(x => type.Name) : query.OrderByDescending(x => type.Name);
            }

            if (!string.IsNullOrWhiteSpace(resourceParameters.SearchQuery))
            {
                query = query.Where(x => x.Name.ToLower().Contains(resourceParameters.SearchQuery.ToLower()));
            }
            
            if(resourceParameters.AddressType > 0)
            {
                query = query.Where(x => x.StudentAddresses.All(c => (int)c.AddressType == resourceParameters.AddressType));
            }
            return PagedList<Student>.Create(await query.ToListAsync(), resourceParameters.PageNumber, resourceParameters.PageSize);
        }
    }
}
