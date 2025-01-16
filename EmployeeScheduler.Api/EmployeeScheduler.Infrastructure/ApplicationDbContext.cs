using Microsoft.EntityFrameworkCore;
using EmployeeScheduler.Application.Context;
using EmployeeScheduler.Domain.Entities;
using System.Reflection;

namespace EmployeeScheduler.Infrastructure
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options)
        {
        }

        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<Shift> Shifts { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}