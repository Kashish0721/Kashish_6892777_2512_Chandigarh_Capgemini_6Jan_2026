using System.ComponentModel.DataAnnotations;

namespace EmployeeProjectManagement.Models
{
    public class Project
    {
        public int ProjectId { get; set; }

        [Required]
        public string Title { get; set; }

        public ICollection<EmployeeProject>? EmployeeProjects { get; set; }
    }
}