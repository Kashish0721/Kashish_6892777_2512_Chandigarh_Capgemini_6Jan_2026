public class Appointment
{
    public int AppointmentId { get; set; }

    public int PatientId { get; set; }
    public int DoctorId { get; set; }

    public DateTime AppointmentDate { get; set; }
    public string Status { get; set; }

    public User Patient { get; set; }
    public Doctor Doctor { get; set; }

    public Prescription Prescription { get; set; }
public Bill Bill { get; set; }
}