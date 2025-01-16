using EmployeeScheduler.DataContracts;
using Microsoft.AspNetCore.Mvc;
using EmployeeScheduler.Api;
using EmployeeScheduler.Application.Services;

namespace ScheduleApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ShiftController : ControllerBase
    {
        private readonly IShiftService _shiftService;
        public ShiftController(IShiftService shiftService)
        {
            _shiftService = shiftService;
        }

        [HttpGet("getemployeesshifts")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _shiftService.GetAllAsync();
            return Ok(new GetEmployeesShiftsResponse { EmployeesShifts = result.Select(x => x.Map()) });
        }

        [HttpGet("getemployeeshifts")]
        public async Task<IActionResult> GetShiftsForEmployee(int employeeId)
        {
            var result = await _shiftService.GetEmployeeShiftsAsync(employeeId);
            return Ok(new GetEmployeeShiftsResponse { EmployeeShifts = result.Select(x => x.Map()) });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var shift = await _shiftService.GetAsync(id);
            if (shift == null)
            {
                return NotFound();
            }

            return Ok(new GetEmployeeShiftResponse { Shift = shift.Map() });
        }

        [HttpPost]
        public async Task<IActionResult> InsertAsync(InsertUpdateShiftRequest insertUpdateShiftRequest)
        {
            if (insertUpdateShiftRequest.StartDate >= insertUpdateShiftRequest.EndDate)
            {
                return BadRequest("Shift start time cannot be later than or equal to shift end time.");
            }

            var id = await _shiftService.InsertAsync(insertUpdateShiftRequest.Map());

            return Created(string.Empty, new { id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, InsertUpdateShiftRequest insertUpdateShiftRequest)
        {
            if (insertUpdateShiftRequest.StartDate >= insertUpdateShiftRequest.EndDate)
            {
                return BadRequest("Shift start time cannot be later than or equal to shift end time.");
            }

            await _shiftService.UpdateAsync(id, insertUpdateShiftRequest.Map());

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _shiftService.DeleteAsync(id);

            return NoContent();
        }
    }
}