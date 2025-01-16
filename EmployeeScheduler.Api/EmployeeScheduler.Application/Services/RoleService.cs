using Microsoft.EntityFrameworkCore;
using EmployeeScheduler.Application.Context;
using EmployeeScheduler.Application.Models;

namespace EmployeeScheduler.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public RoleService(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await _applicationDbContext.Roles
                .Select(x => new Role
                {
                    Id = x.Id,
                    Name = x.Name,
                }).ToListAsync();
        }

        public async Task<IEnumerable<Role>> GetRolesForEmployeeAsync(int employeeId)
        {
            return await _applicationDbContext.Roles
                .Include(x => x.EmployeeRoles)
                .Where(x => x.EmployeeRoles.Any(er => er.EmployeeId == employeeId))
                .Select(x => new Role
                {
                    Id = x.Id,
                    Name = x.Name,
                })
                .ToListAsync();
        }
    }
}