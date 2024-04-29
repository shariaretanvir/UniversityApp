using AuthApp.Core.Features.Authentication.Login;
using AuthApp.Domain.Entities;
using AuthApp.Infrastructure.Common;
using AuthApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthApp.Infrastructure.Repository
{
    public class LoginRepository : GenericRepository<ApplicationUser, Guid>, IRepository
    {
        public LoginRepository(AuthAppDbContext _dbContext) : base(_dbContext)
        {
        }

        public async Task<ApplicationUser> GetLoggedInUser(string username, string email, string password)
        {
            IQueryable<ApplicationUser> query = _dbContext.Set<ApplicationUser>()
                .Where(x => x.UserName.Trim().ToLower() == username.Trim().ToLower() && x.Email.Trim().ToLower() == email.Trim().ToLower() && x.Password.Trim().ToLower() == password.Trim().ToLower() && x.IsActive == true);

            return await query.FirstOrDefaultAsync();
        }
    }
}
