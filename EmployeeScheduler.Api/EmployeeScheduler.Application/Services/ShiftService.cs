using EmployeeScheduler.Application.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EmployeeScheduler.Application.Services
{
    public class ShiftService : IShiftService
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public ShiftService(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<Models.Shift>> GetAllAsync()
        {
            return await _applicationDbContext.Shifts
                .Include(s => s.Employee)
                .Include(s => s.Role)
                .Select(x => new Models.Shift
                {
                    Id = x.Id,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    Role = x.Role,
                    RoleId = x.RoleId,
                    EmployeeId = x.EmployeeId,
                    Employee = new Models.Employee
                    {
                        Id = x.Employee.Id,
                        Name = x.Employee.Name,
                    },
                })
                .Where(GetCurrentWeekFilter())
                .ToListAsync();
        }

        public async Task<Models.Shift?> GetAsync(int id)
        {
            var shift = await _applicationDbContext.Shifts
                .Include(s => s.Role)
                .Include(s => s.Employee)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (shift == null)
            {
                return null;
            }

            return new Models.Shift
            {
                Id = id,
                StartDate = shift.StartDate,
                EndDate = shift.EndDate,
                Role = shift.Role,
                RoleId = shift.RoleId,
                Employee = new Models.Employee
                {
                    Id = shift.Employee.Id,
                    Name = shift.Employee.Name,
                },
                EmployeeId = shift.EmployeeId,
            };
        }

        public async Task<IEnumerable<Models.Shift>> GetEmployeeShiftsAsync(int employeeId)
        {
            return await _applicationDbContext.Shifts
                .Select(x => new Models.Shift
                {
                    Id = x.Id,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    RoleId = x.RoleId,
                    EmployeeId = x.EmployeeId,
                })
                .Where(x => x.EmployeeId == employeeId)
                .Where(GetCurrentWeekFilter())
                .ToListAsync();
        }

        public async Task<int> InsertAsync(Models.Shift insertShift)
        {
            var shift = new Domain.Entities.Shift
            {
                StartDate = insertShift.StartDate,
                EndDate = insertShift.EndDate,
                EmployeeId = insertShift.EmployeeId,
                RoleId = insertShift.RoleId
            };

            await _applicationDbContext.Shifts.AddAsync(shift);

            await _applicationDbContext.SaveChangesAsync();

            return shift.Id;
        }

        public async Task UpdateAsync(int id, Models.Shift updateShift)
        {
            var shift = await _applicationDbContext.Shifts.SingleAsync(x => x.Id == id);

            shift.StartDate = updateShift.StartDate;
            shift.EndDate = updateShift.EndDate;
            shift.RoleId = updateShift.RoleId;

            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var shift = await _applicationDbContext.Shifts.SingleAsync(x => x.Id == id);

            _applicationDbContext.Remove(shift);
            await _applicationDbContext.SaveChangesAsync();
        }

        private static Expression<Func<Models.Shift, bool>> GetCurrentWeekFilter()
        {
            var today = DateTime.Today;
            var startOfWeek = today.AddDays(-(int)today.DayOfWeek + (int)DayOfWeek.Monday);
            var endOfWeek = startOfWeek.AddDays(7).AddTicks(-1);

            var parameter = Expression.Parameter(typeof(Models.Shift), "x");

            var startOfWeekExpr = Expression.Constant(startOfWeek);
            var endOfWeekExpr = Expression.Constant(endOfWeek);

            var startDateExpr = Expression.Property(parameter, "StartDate");

            var greaterThanOrEqual = Expression.GreaterThanOrEqual(startDateExpr, startOfWeekExpr);
            var lessThanOrEqual = Expression.LessThanOrEqual(startDateExpr, endOfWeekExpr);

            var andExpr = Expression.AndAlso(greaterThanOrEqual, lessThanOrEqual);

            return Expression.Lambda<Func<Models.Shift, bool>>(andExpr, parameter);
        }
    }
}