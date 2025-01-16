namespace EmployeeScheduler.DataContracts
{
    public class GetEmployeesShiftsResponse
    {
        public IEnumerable<Shift> EmployeesShifts { get; set; }
    }
}