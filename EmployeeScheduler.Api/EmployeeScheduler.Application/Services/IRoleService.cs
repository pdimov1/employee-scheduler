using EmployeeScheduler.Application.Models;

namespace EmployeeScheduler.Application.Services
{
    /// <summary>
    /// Service interface for managing role operations.
    /// </summary>
    public interface IRoleService
    {
        /// <summary>
        /// Retrieves all roles available in the system.
        /// </summary>
        /// <returns>A task that returns a collection of all roles.</returns>
        Task<IEnumerable<Role>> GetAllAsync();

        /// <summary>
        /// Retrieves all roles assigned to a specific employee.
        /// </summary>
        /// <param name="employeeId">The Id of the employee whose roles are to be retrieved.</param>
        /// <returns>A task that returns a collection of roles assigned to the specified employee.</returns>
        Task<IEnumerable<Role>> GetRolesForEmployeeAsync(int employeeId);
    }
}