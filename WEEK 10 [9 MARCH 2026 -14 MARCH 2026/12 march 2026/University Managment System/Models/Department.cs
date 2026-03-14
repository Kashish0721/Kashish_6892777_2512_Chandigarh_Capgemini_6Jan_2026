using System.ComponentModel.DataAnnotations;

public class Department
{
    [Key]
    public int DepartmentId { get; set; }

    public string Name { get; set; }

    public decimal Budget { get; set; }

    public ICollection<Instructor> Instructors { get; set; }
}