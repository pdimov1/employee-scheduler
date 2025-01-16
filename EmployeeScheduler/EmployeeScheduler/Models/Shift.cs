namespace EmployeeScheduler.Models
{
    public class Shift
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int EmployeeId { get; set; }

        public Employee Employee { get; set; }

        public Role Role { get; set; }

        public int RoleId { get; set; }
    }
}