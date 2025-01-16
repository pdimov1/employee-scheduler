using EmployeeScheduler.DataContracts;

namespace EmployeeScheduler.Clients.Abstractions
{
    public interface IShiftClient
    {
        Task<GetEmployeesShiftsResponse> GetEmployeesShiftsAsync();

        Task<GetEmployeeShiftResponse> GetEmployeeShiftAsync(int id);

        Task<GetEmployeeShiftsResponse> GetEmployeeShiftsAsync(int employeeId);

        Task InsetUpdateShiftAsync(InsertUpdateShiftRequest request);

        Task DeleteShiftAsync(int shiftId);
    }
}