using EmployeeScheduler.DataContracts;

namespace EmployeeScheduler.Clients.Abstractions
{
    public interface IRoleClient
    {
        Task<GetRolesResponse> GetRolesForEmployeeAsync(int employeeId);
    }
}