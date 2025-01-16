using EmployeeScheduler.Clients.Abstractions;
using EmployeeScheduler.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmployeeScheduler.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IShiftClient _shiftClient;

        public List<Shift> Shifts { get; set; } = new List<Shift>();

        public IndexModel(IShiftClient shiftClient)
        {
            _shiftClient = shiftClient;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var employeesShiftsResponse = await _shiftClient.GetEmployeesShiftsAsync();

            Shifts = employeesShiftsResponse.EmployeesShifts.Select(x => new Shift
            {
                Id = x.Id,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                EmployeeId = x.Employee.Id,
                Employee = new Employee
                {
                    Id = x.Employee.Id,
                    Name = x.Employee.Name,
                },
                RoleId = x.Role.Id,
                Role = new Role
                {
                    Id = x.Role.Id,
                    Name = x.Role.Name
                }
            }).ToList();

            return Page();
        }
    }
}
