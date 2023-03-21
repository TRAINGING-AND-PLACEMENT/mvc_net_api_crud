using System.ComponentModel.DataAnnotations;

namespace SCET_workshop.Models
{
    public class Employee
    {
        public int Employeeid { get; set; }

        [Required(ErrorMessage = "please enter name")]
        [StringLength(26, MinimumLength = 3, ErrorMessage = "length invalid")]
        [Display(Name = "designation")]
        public string EmployeeName { get; set; }

        [Required(ErrorMessage = "please enter designation")]
        [StringLength(26, MinimumLength = 3, ErrorMessage = "length invalid")]
        [Display(Name = "designation")]
        public string Employeedesignation { get; set; }

        [Required(ErrorMessage = "please enter department")]
        [StringLength(26, MinimumLength = 3, ErrorMessage = "length invalid")]
        [Display(Name = "designation")]
        public string EmployeeDepartment { get; set; }
        public string Createddatetime { get; set; }
    }
}
