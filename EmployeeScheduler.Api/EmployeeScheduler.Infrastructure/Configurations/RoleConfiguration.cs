using EmployeeScheduler.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeScheduler.Api.Infrastructure.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable(nameof(Role), "dbo");

            builder.Property(p=> p.Name)
                .HasMaxLength(30)
            .IsRequired();

            builder.HasData(
              new Role { Id = 1, Name = "Cashier" },
              new Role { Id = 2, Name = "Waiter" },
              new Role { Id = 3, Name = "Manager" }
          );
        }
    }
}