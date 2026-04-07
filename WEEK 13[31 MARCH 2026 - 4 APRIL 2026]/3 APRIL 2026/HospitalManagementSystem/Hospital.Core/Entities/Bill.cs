public class Bill
{
    public int BillId { get; set; }

    public int AppointmentId { get; set; }

    public decimal ConsultationFee { get; set; }
    public decimal MedicineCharges { get; set; }

    public string PaymentStatus { get; set; } // Paid / Unpaid

    public Appointment Appointment { get; set; }
}