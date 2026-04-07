using System.ComponentModel.DataAnnotations;

namespace HealthcareSystem.Models.Entities;

public class Specialization
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(300)]
    public string? Description { get; set; }

    // Navigation
    public ICollection<DoctorSpecialization> DoctorSpecializations { get; set; } = new List<DoctorSpecialization>();
}

// Join table for Many-to-Many
public class DoctorSpecialization
{
    public int DoctorId { get; set; }
    public int SpecializationId { get; set; }

    public Doctor Doctor { get; set; } = null!;
    public Specialization Specialization { get; set; } = null!;
}
