namespace EmployeeScheduler.DataContracts
{
    public class Shift
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public Employee Employee { get; set; }

        public Role Role { get; set; }
    }
}