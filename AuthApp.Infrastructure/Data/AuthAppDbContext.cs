using AuthApp.Domain.Common;
using AuthApp.Domain.Entities;
using AuthApp.Infrastructure.Data.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthApp.Infrastructure.Data
{
    public class AuthAppDbContext : DbContext
    {
        public AuthAppDbContext(DbContextOptions options) : base(options)
        {
            
        }

        //dbset
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entity in ChangeTracker.Entries<IAudit>())
            {
                if(entity.State == EntityState.Added)
                {
                    entity.Entity.SetCreatedOn(DateTime.UtcNow);
                }
                if(entity.State == EntityState.Modified)
                {
                    entity.Entity.SetModifiedOn(DateTime.UtcNow);
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        //configuration

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new ApplicationUserEntityConfiguration().Configure(modelBuilder.Entity<ApplicationUser>());
        }
    }
}
