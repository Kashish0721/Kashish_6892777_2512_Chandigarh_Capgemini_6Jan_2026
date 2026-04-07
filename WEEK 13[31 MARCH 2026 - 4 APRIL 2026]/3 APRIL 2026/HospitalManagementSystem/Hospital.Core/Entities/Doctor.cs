public class Doctor
{
    public int DoctorId { get; set; }

    public int UserId { get; set; }
    public int DepartmentId { get; set; }

    public string Specialization { get; set; }
    public int ExperienceYears { get; set; }
    public string Availability { get; set; }

    public User User { get; set; }
    public Department Department { get; set; }
}