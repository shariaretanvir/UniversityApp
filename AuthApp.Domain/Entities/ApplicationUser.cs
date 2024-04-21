using AuthApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthApp.Domain.Entities
{
    public class ApplicationUser : Entity<Guid>, IAudit
    {
        public string UserName { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public bool IsActive { get; private set; }

        public ApplicationUser(Guid id, string userName, string email, string password, bool isActive)
        {
            Id = id;
            UserName = userName;
            Email = email;
            Password = password;
            IsActive = isActive;
        }

        #region audit
        public DateTime CreatedOn { get; private set; }

        public DateTime ModifiedOn { get; private set; }

        public void SetCreatedOn(DateTime createdOn)
        {
            CreatedOn = createdOn;
        }

        public void SetModifiedOn(DateTime modifiedOn)
        {
            ModifiedOn = modifiedOn;
        }
        #endregion

    }
}
