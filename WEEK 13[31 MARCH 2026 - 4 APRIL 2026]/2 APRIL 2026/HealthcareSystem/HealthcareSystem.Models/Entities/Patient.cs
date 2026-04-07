using System.ComponentModel.DataAnnotations;

namespace HealthcareSystem.Models.Entities;

public class Patient
{
    public int Id { get; set; }

    public int UserId { get; set; }

    [MaxLength(15)]
    public string? PhoneNumber { get; set; }

    public DateTime? DateOfBirth { get; set; }

    [MaxLength(10)]
    public string? Gender { get; set; }

    [MaxLength(500)]
    public string? Address { get; set; }

    [MaxLength(200)]
    public string? MedicalHistory { get; set; }

    public string? BloodGroup { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public User User { get; set; } = null!;
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
