using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Infrastructure.Data.Configuration
{
    public class StudentAddressEntityConfiguration : IEntityTypeConfiguration<StudentAddress>
    {
        public void Configure(EntityTypeBuilder<StudentAddress> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.AddressType).IsRequired().HasConversion<int>(); //.HasColumnType("nvarchar(20)");
            builder.Property(x=>x.FullAddress).IsRequired();
            builder.HasOne(x => x.Student).WithMany().HasForeignKey("StudentId").OnDelete(DeleteBehavior.Cascade);
        }
    }
}
