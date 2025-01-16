using EmployeeScheduler.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace EmployeeScheduler.Infrastructure.Configurations
{
    public class EmployeeRoleConfiguration : IEntityTypeConfiguration<EmployeeRole>
    {
        public void Configure(EntityTypeBuilder<EmployeeRole> builder)
        {
            builder.ToTable(nameof(EmployeeRole), "dbo");

            builder.HasKey(er => new { er.EmployeeId, er.RoleId });

            builder.HasOne(er => er.Employee)
                .WithMany(e => e.EmployeeRoles)
                .HasForeignKey(er => er.EmployeeId);

            builder.HasOne(er => er.Role)
                .WithMany(r => r.EmployeeRoles)
                .HasForeignKey(er => er.RoleId);

            builder.HasData(
                new EmployeeRole { EmployeeId = 1, RoleId = 1 },
                new EmployeeRole { EmployeeId = 1, RoleId = 2 },
                new EmployeeRole { EmployeeId = 2, RoleId = 2 },
                new EmployeeRole { EmployeeId = 2, RoleId = 3 }
         );
        }
    }
}