using EmployeeScheduler.DataContracts;

namespace EmployeeScheduler.Api
{
    internal static class MappingExtensions
    {
        internal static Application.Models.Shift Map(this InsertUpdateShiftRequest entity)
        {
            return new Application.Models.Shift
            {
                EmployeeId = entity.EmployeeId,
                EndDate = entity.EndDate,
                Id = entity.Id,
                RoleId = entity.RoleId,
                StartDate = entity.StartDate,
            };
        }

        internal static Shift Map(this Application.Models.Shift entity)
        {
            return new Shift
            {
                Id = entity.Id,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                Employee = new Employee
                {
                    Id = entity.EmployeeId,
                    Name = entity.Employee?.Name,
                },
                Role = new Role
                {
                    Id = entity.RoleId,
                    Name = entity.Role?.Name
                }
            };
        }

        internal static Employee Map(this Application.Models.Employee entity)
        {
            return new Employee
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        internal static Role Map(this Application.Models.Role entity)
        {
            return new Role
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }
    }
}
