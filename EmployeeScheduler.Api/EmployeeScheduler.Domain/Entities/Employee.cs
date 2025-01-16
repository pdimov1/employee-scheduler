namespace EmployeeScheduler.Domain.Entities
{
    public class Employee
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<EmployeeRole> EmployeeRoles { get; set; } = new List<EmployeeRole>();
    }
}