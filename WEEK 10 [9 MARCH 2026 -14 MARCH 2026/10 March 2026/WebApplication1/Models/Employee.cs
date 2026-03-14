using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("tblEmployee")]
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public int salary { get; set; }

        public string? Department { get; set; }

        public string? Phone { get; set; }
        public DateTime? DateOfJoining { get; set; }

    }
}