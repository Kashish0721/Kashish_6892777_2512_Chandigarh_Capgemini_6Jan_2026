using System.ComponentModel.DataAnnotations;

namespace EmployeeProjectManagement.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Employee>? Employees { get; set; }
    }
}