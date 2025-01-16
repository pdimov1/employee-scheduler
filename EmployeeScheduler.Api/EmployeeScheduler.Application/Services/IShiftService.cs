using EmployeeScheduler.Application.Models;

namespace EmployeeScheduler.Application.Services
{
    /// <summary>
    /// Service interface for managing shift operations.
    /// </summary>
    public interface IShiftService
    {
        /// <summary>
        /// Retrieves a shift by its unique Identifier.
        /// </summary>
        /// <param name="Id">The Id of the shift to retrieve.</param>
        /// <returns>A task that returns the shift if found, otherwise null.</returns>
        Task<Shift?> GetAsync(int id);

        /// <summary>
        /// Retrieves all shifts.
        /// </summary>
        /// <returns>A task that returns a collection of all shifts.</returns>
        Task<IEnumerable<Shift>> GetAllAsync();

        /// <summary>
        /// Retrieves all shifts assigned to a specific employee.
        /// </summary>
        /// <param name="employeeId">The Id of the employee.</param>
        /// <returns>A task that returns a collection of shifts for the given employee.</returns>
        Task<IEnumerable<Shift>> GetEmployeeShiftsAsync(int employeeId);

        /// <summary>
        /// Inserts a new shift into the system.
        /// </summary>
        /// <param name="insertShift">The shift entity to insert.</param>
        /// <returns>A task that returns the Id of the newly created shift.</returns>
        Task<int> InsertAsync(Shift insertShift);

        /// <summary>
        /// Updates an existing shift.
        /// </summary>
        /// <param name="Id">The Id of the shift to update.</param>
        /// <param name="updateShift">The updated shift entity.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task UpdateAsync(int id, Shift updateShift);

        /// <summary>
        /// Deletes a shift by its Id.
        /// </summary>
        /// <param name="Id">The Id of the shift to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task DeleteAsync(int id);

    }
}