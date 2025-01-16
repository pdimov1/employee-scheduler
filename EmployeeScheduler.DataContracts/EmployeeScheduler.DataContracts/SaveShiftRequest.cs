namespace EmployeeScheduler.DataContracts
{
    public class InsertUpdateShiftRequest
    {
        public int Id { get; set; }

        public int RoleId { get; set; }

        public int EmployeeId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}