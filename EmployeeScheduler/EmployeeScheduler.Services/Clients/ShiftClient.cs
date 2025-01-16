using EmployeeScheduler.Clients.Abstractions;
using EmployeeScheduler.DataContracts;
using Microsoft.Extensions.Logging;

namespace EmployeeScheduler.Clients.Clients
{
    public class ShiftClient : ClientBase, IShiftClient
    {
        public ShiftClient(IHttpClientFactory httpClientFactory, ILogger<ShiftClient> logger)
            : base(httpClientFactory, logger)
        {
        }

        public async Task<GetEmployeesShiftsResponse> GetEmployeesShiftsAsync()
        {
            return await GetAsync<GetEmployeesShiftsResponse>($"shift/getemployeesshifts");
        }

        public async Task<GetEmployeeShiftResponse> GetEmployeeShiftAsync(int id)
        {
            return await GetAsync<GetEmployeeShiftResponse>($"shift/{id}");
        }

        public async Task<GetEmployeeShiftsResponse> GetEmployeeShiftsAsync(int employeeId)
        {
            return await GetAsync<GetEmployeeShiftsResponse>($"shift/getemployeeshifts?employeeId={employeeId}");
        }

        public async Task InsetUpdateShiftAsync(InsertUpdateShiftRequest request)
        {
            if (request.Id != 0)
            {
                await SendMessageAsync($"shift/{request.Id}", request, HttpMethod.Put);
            }
            else
            {
                await SendMessageAsync($"shift", request, HttpMethod.Post);
            }
        }

        public async Task DeleteShiftAsync(int shiftId)
        {
            await SendMessageAsync($"shift/{shiftId}", null, HttpMethod.Delete);
        }

        
    }
}