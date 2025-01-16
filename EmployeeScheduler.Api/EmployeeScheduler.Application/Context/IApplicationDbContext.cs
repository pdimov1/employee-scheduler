using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using EmployeeScheduler.Domain.Entities;

namespace EmployeeScheduler.Application.Context
{
    public interface IApplicationDbContext
    {
        DbSet<Shift> Shifts { get; set; }

        DbSet<Role> Roles { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        EntityEntry Remove(object entity);
    }
}