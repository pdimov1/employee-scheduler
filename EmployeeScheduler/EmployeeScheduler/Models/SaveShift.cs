using System.ComponentModel.DataAnnotations;

namespace EmployeeScheduler.Models
{
    public class InsertUpdateShift
    {
        public int Id { get; set; }

        [Display(Name = "Start time"), DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Display(Name = "End time"), DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        [Display(Name = "Role")]
        public int RoleId { get; set; }

        public int EmployeeId { get; set; }
    }
}