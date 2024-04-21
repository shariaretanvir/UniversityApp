using AuthApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthApp.Core.Features.Authentication.Login
{
    public interface IRepository
    {
        Task<Entities.ApplicationUser> GetLoggedInUser(string username, string email, string password);
    }
}
