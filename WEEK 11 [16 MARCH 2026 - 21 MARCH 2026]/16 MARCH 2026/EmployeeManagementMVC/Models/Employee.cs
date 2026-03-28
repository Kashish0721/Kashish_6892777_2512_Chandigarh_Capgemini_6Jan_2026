using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementMVC.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Range(21, 65)]
        public int Age { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Department { get; set; }
    }
}