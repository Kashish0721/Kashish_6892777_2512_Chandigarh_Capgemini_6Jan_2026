using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthcareSystem.Models.Entities;

public class Bill
{
    public int Id { get; set; }

    public int AppointmentId { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal ConsultationFee { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal MedicineCharges { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public decimal TotalAmount { get; set; }

    [MaxLength(20)]
    public string PaymentStatus { get; set; } = "Unpaid"; // Paid, Unpaid

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? PaidAt { get; set; }

    // Navigation
    public Appointment Appointment { get; set; } = null!;
}
