namespace EmployeeScheduler.DataContracts
{
    public class GetEmployeeShiftsResponse
    {
        public IEnumerable<Shift> EmployeeShifts { get; set; }
    }
}