using AuthApp.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthApp.Domain.Entities
{
    public class UserToken : Entity<Guid>, IAudit
    {
        public Guid UserId { get; private set; }
        //public string AccessToken { get; private set; }
        public string RefreshToken { get; private set; }
        public DateTime RefreshTokenExpiryDateTime { get; private set; }
        public bool IsActive { get; private set; }
        public ApplicationUser ApplicationUser { get; private set; }

        public UserToken(Guid id, Guid userId, string refreshToken, DateTime refreshTokenExpiryDateTime, bool isActive)
        {
            Id = id;
            UserId = userId;
            //AccessToken = accessToken;
            RefreshToken = refreshToken;
            RefreshTokenExpiryDateTime = refreshTokenExpiryDateTime;
            IsActive = isActive;
        }

        public void SetRefreshToken(string refreshToken)
        {
            RefreshToken = refreshToken;
        }

        public void SetRefreshTokenExpiry(DateTime refreshTokenexpiry)
        {
            RefreshTokenExpiryDateTime = refreshTokenexpiry;
        }

        public void SetIsActive(bool isActive)
        {
            IsActive = isActive;
        }

        #region audit_properties
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
