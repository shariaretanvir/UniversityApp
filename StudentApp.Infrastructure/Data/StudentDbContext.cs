using Microsoft.EntityFrameworkCore;
using StudentApp.Domain.Common;
using StudentApp.Domain.Entities;
using StudentApp.Infrastructure.Data.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Infrastructure.Data
{
    public class StudentDbContext: DbContext
    {
        public StudentDbContext(DbContextOptions options) : base(options) { }
        #region dbsets
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentAddress> StudentAddresses { get; set; }

        #endregion



        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entity in ChangeTracker.Entries<IAudit>())
            {
                if(entity.State == EntityState.Added)
                {
                    entity.Entity.SetCreatedOn(DateTime.Now);
                }
                if(entity.State == EntityState.Modified)
                {
                    entity.Entity.SetModifiedOn(DateTime.Now);
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new StudentEntityConfiguration().Configure(modelBuilder.Entity<Student>());
            new StudentAddressEntityConfiguration().Configure(modelBuilder.Entity<StudentAddress>());
        }

    }
}
