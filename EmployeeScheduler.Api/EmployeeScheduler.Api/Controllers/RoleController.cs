using EmployeeScheduler.DataContracts;
using Microsoft.AspNetCore.Mvc;
using EmployeeScheduler.Application.Services;

namespace EmployeeScheduler.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet("getemployeeroles")]
        public async Task<IActionResult> GetRolesForEmployeeAsync(int employeeId)
        {
            var result = await _roleService.GetRolesForEmployeeAsync(employeeId);
            return Ok(new GetRolesResponse { Roles = result.Select(x => x.Map()) });
        }
    }
}