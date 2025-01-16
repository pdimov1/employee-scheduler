using EmployeeScheduler.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeScheduler.Api.Infrastructure.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.ToTable(nameof(Employee), "dbo");

            builder.Property(p=> p.Name)
                .HasMaxLength(200)
            .IsRequired();

           builder.HasData(
              new Employee { Id = 1, Name = "John Doe" },
              new Employee { Id = 2, Name = "Jane Smith" }
          );
        }
    }
}