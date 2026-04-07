using System.ComponentModel.DataAnnotations;

namespace HealthcareSystem.Models.Entities;

public class Doctor
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int? DepartmentId { get; set; }

    [MaxLength(20)]
    public string? LicenseNumber { get; set; }

    [MaxLength(15)]
    public string? PhoneNumber { get; set; }

    public int? YearsOfExperience { get; set; }

    public decimal? ConsultationFee { get; set; }

    [MaxLength(300)]
    public string? Biography { get; set; }

    public bool IsAvailable { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public User User { get; set; } = null!;
    public Department? Department { get; set; }
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    public ICollection<DoctorSpecialization> DoctorSpecializations { get; set; } = new List<DoctorSpecialization>();
    public ICollection<Prescription> Prescriptions { get; set; } = new List<Prescription>();
}
