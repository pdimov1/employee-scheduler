using EmployeeScheduler.Clients.Abstractions;
using EmployeeScheduler.DataContracts;
using EmployeeScheduler.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;

namespace EmployeeScheduler.Pages
{
    public class InsertUpdateShiftModel : PageModel
    {
        private readonly IShiftClient _shiftClient;
        private readonly IRoleClient _roleClient;

        [BindProperty]
        public InsertUpdateShift Shift { get; set; } = default!;

        public IEnumerable<Models.Role> Roles { get; set; }

        public InsertUpdateShiftModel(IShiftClient shiftClient, IRoleClient roleClient)
        {
            _shiftClient = shiftClient;
            _roleClient = roleClient;
        }

        public async Task<IActionResult> OnGetAsync(int id, int eid)
        {
            if (id > 0)
            {
                var shiftResponse = await _shiftClient.GetEmployeeShiftAsync(id);
                if (shiftResponse == null || shiftResponse.Shift == null)
                {
                    return NotFound();
                }

                Shift = new InsertUpdateShift
                {
                    Id = shiftResponse.Shift.Id,
                    StartDate = shiftResponse.Shift.StartDate,
                    EndDate = shiftResponse.Shift.EndDate,
                    RoleId = shiftResponse.Shift.Role.Id,
                    EmployeeId = shiftResponse.Shift.Employee.Id
                };

            }
            else
            {
                Shift = new InsertUpdateShift()
                {
                    EmployeeId = eid,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now,
                };
            }

            await LoadRoles(Shift.EmployeeId);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string day)
        {
            await LoadRoles(Shift.EmployeeId);

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var shiftDate = GetShiftDate(day);
            var startDate = shiftDate.Add(Shift.StartDate.TimeOfDay);
            var endDate = shiftDate.Add(Shift.EndDate.TimeOfDay);

            if (startDate >= endDate)
            {
                ModelState.AddModelError("Shift.EndDate", "Shift start time cannot be later than or equal to shift end time.");
                return Page();
            }

            if (await HasOverlappingShiftsAsync(Shift.EmployeeId, startDate, endDate, Shift.Id))
            {
                ModelState.AddModelError(string.Empty, "The shift overlaps with an existing shift for this employee.");
                return Page();
            }

            await _shiftClient.InsetUpdateShiftAsync(new InsertUpdateShiftRequest
            {
                StartDate = startDate,
                EndDate = endDate,
                Id = Shift.Id,
                RoleId = Shift.RoleId,
                EmployeeId = Shift.EmployeeId,
            });

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            await _shiftClient.DeleteShiftAsync(Shift.Id);
            
            return RedirectToPage("./Index");
        }

        private async Task LoadRoles(int employeeId)
        {
            var rolesResponse = await _roleClient.GetRolesForEmployeeAsync(employeeId);

            Roles = rolesResponse.Roles.Select(x => new Models.Role
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();
        }

        private async Task<bool> HasOverlappingShiftsAsync(int employeeId, DateTime startDate, DateTime endDate, int? shiftId = null)
        {
            var shiftsResponse = await _shiftClient.GetEmployeeShiftsAsync(employeeId);
            if (shiftsResponse.EmployeeShifts != null)
            {
                return shiftsResponse.EmployeeShifts.Any(s => s.Id != shiftId && s.StartDate.ToUniversalTime() < endDate.ToUniversalTime() && s.EndDate.ToUniversalTime() > startDate.ToUniversalTime());
            }

            return false;
        }

        private DateTime GetShiftDate(string selectedDate)
        {
            var today = DateTime.Today;
            var dayOfWeek = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), selectedDate);

            return today.AddDays((int)dayOfWeek - (int)today.DayOfWeek);
        }
    }
}