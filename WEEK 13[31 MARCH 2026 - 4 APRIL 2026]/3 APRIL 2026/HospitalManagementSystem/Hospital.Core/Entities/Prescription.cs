public class Prescription
{
    public int PrescriptionId { get; set; }

    public int AppointmentId { get; set; }
    public string Diagnosis { get; set; }
    public string Medicines { get; set; }

    public Appointment Appointment { get; set; }
}