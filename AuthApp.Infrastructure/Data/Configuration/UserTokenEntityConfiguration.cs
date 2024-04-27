using AuthApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthApp.Infrastructure.Data.Configuration
{
    public class UserTokenEntityConfiguration : IEntityTypeConfiguration<UserToken>
    {
        public void Configure(EntityTypeBuilder<UserToken> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.UserId).IsRequired();
            builder.Property(x => x.AccessToken).IsRequired();
            builder.Property(x => x.RefreshToken).IsRequired();
            builder.Property(x => x.RefreshTokenExpiryDateTime).IsRequired();
            builder.Property(x=>x.IsActive).HasDefaultValue(true);
        }
    }
}
