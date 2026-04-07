using System.ComponentModel.DataAnnotations;

namespace HealthcareSystem.Models.Entities;

public class Department
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(300)]
    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
}
