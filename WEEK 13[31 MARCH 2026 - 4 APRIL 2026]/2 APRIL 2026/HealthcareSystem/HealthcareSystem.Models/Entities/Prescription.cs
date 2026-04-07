using System.ComponentModel.DataAnnotations;

namespace HealthcareSystem.Models.Entities;

public class Prescription
{
    public int Id { get; set; }

    public int AppointmentId { get; set; }

    public int DoctorId { get; set; }

    public int PatientId { get; set; }

    [MaxLength(1000)]
    public string? Diagnosis { get; set; }

    [MaxLength(500)]
    public string? Instructions { get; set; }

    public DateTime IssuedDate { get; set; } = DateTime.UtcNow;

    public DateTime? ValidUntil { get; set; }

    // Navigation
    public Appointment Appointment { get; set; } = null!;
    public Doctor Doctor { get; set; } = null!;
    public ICollection<PrescriptionMedicine> PrescriptionMedicines { get; set; } = new List<PrescriptionMedicine>();
}

public class Medicine
{
    public int Id { get; set; }

    [Required, MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(100)]
    public string? Generic { get; set; }

    [MaxLength(50)]
    public string? Dosage { get; set; }

    [MaxLength(200)]
    public string? Description { get; set; }

    public ICollection<PrescriptionMedicine> PrescriptionMedicines { get; set; } = new List<PrescriptionMedicine>();
}

public class PrescriptionMedicine
{
    public int PrescriptionId { get; set; }
    public int MedicineId { get; set; }

    [MaxLength(100)]
    public string? Dosage { get; set; }

    [MaxLength(100)]
    public string? Frequency { get; set; }

    [MaxLength(100)]
    public string? Duration { get; set; }

    public Prescription Prescription { get; set; } = null!;
    public Medicine Medicine { get; set; } = null!;
}
