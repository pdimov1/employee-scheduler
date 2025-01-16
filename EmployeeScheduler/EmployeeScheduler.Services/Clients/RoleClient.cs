using EmployeeScheduler.Clients.Abstractions;
using EmployeeScheduler.DataContracts;
using Microsoft.Extensions.Logging;

namespace EmployeeScheduler.Clients.Clients
{
    public class RoleClient : ClientBase, IRoleClient
    {
        public RoleClient(IHttpClientFactory httpClientFactory, ILogger<ShiftClient> logger) 
            : base(httpClientFactory, logger)
        {
        }

        public async Task<GetRolesResponse> GetRolesForEmployeeAsync(int employeeId)
        {
            return await GetAsync<GetRolesResponse>($"role/getemployeeroles?employeeId={employeeId}");
        }
    }
}