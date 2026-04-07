using System.ComponentModel.DataAnnotations;

namespace HealthcareSystem.Models.Entities;

public class Appointment
{
    public int Id { get; set; }

    public int PatientId { get; set; }

    public int DoctorId { get; set; }

    [Required]
    public DateTime AppointmentDate { get; set; }

    [MaxLength(20)]
    public string Status { get; set; } = "Pending"; // Pending, Confirmed, Completed, Cancelled

    [MaxLength(500)]
    public string? Notes { get; set; }

    [MaxLength(500)]
    public string? Symptoms { get; set; }

    public decimal? Fee { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    // Navigation
    public Patient Patient { get; set; } = null!;
    public Doctor Doctor { get; set; } = null!;
    public Prescription? Prescription { get; set; }
    public Bill? Bill { get; set; }
}
