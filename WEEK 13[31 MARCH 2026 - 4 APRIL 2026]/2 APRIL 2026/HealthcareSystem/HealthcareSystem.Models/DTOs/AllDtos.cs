using System.ComponentModel.DataAnnotations;

namespace HealthcareSystem.Models.DTOs;

// ─── Auth DTOs ───────────────────────────────────────────────────────────────

public class LoginDto
{
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required, MinLength(6)]
    public string Password { get; set; } = string.Empty;
}

public class RegisterDto
{
    [Required, MaxLength(100)]
    public string FullName { get; set; } = string.Empty;

    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required, MinLength(6)]
    public string Password { get; set; } = string.Empty;

    [Required, Compare("Password", ErrorMessage = "Passwords do not match.")]
    public string ConfirmPassword { get; set; } = string.Empty;

    [Required]
    public string Role { get; set; } = "Patient";
}

public class AuthResponseDto
{
    public string Token { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public int UserId { get; set; }
    public DateTime Expiry { get; set; }
}

public class RefreshTokenDto
{
    public string Token { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}

// ─── User DTOs ───────────────────────────────────────────────────────────────

public class UserDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class UpdateUserDto
{
    [Required, MaxLength(100)]
    public string FullName { get; set; } = string.Empty;

    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    public bool IsActive { get; set; }
}

// ─── Patient DTOs ─────────────────────────────────────────────────────────────

public class PatientDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Gender { get; set; }
    public string? Address { get; set; }
    public string? MedicalHistory { get; set; }
    public string? BloodGroup { get; set; }
    public int TotalAppointments { get; set; }
}

public class CreatePatientDto
{
    [Phone]
    public string? PhoneNumber { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? Gender { get; set; }

    [MaxLength(500)]
    public string? Address { get; set; }

    [MaxLength(200)]
    public string? MedicalHistory { get; set; }

    public string? BloodGroup { get; set; }
}

public class UpdatePatientDto
{
    [Phone]
    public string? PhoneNumber { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? Gender { get; set; }

    [MaxLength(500)]
    public string? Address { get; set; }

    [MaxLength(200)]
    public string? MedicalHistory { get; set; }

    public string? BloodGroup { get; set; }
}

// ─── Doctor DTOs ─────────────────────────────────────────────────────────────

public class DoctorDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int? DepartmentId { get; set; }
    public string? DepartmentName { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public string? LicenseNumber { get; set; }
    public int? YearsOfExperience { get; set; }
    public decimal? ConsultationFee { get; set; }
    public string? Biography { get; set; }
    public bool IsAvailable { get; set; }
    public List<string> Specializations { get; set; } = new();
    public int TotalAppointments { get; set; }
}

public class CreateDoctorDto
{
    public int? DepartmentId { get; set; }

    [Phone]
    public string? PhoneNumber { get; set; }

    [MaxLength(20)]
    public string? LicenseNumber { get; set; }

    [Range(0, 60)]
    public int? YearsOfExperience { get; set; }

    [Range(0, 100000)]
    public decimal? ConsultationFee { get; set; }

    [MaxLength(300)]
    public string? Biography { get; set; }

    public List<int> SpecializationIds { get; set; } = new();
}

public class UpdateDoctorDto
{
    public int? DepartmentId { get; set; }

    [Phone]
    public string? PhoneNumber { get; set; }

    public int? YearsOfExperience { get; set; }

    [Range(0, 100000)]
    public decimal? ConsultationFee { get; set; }

    [MaxLength(300)]
    public string? Biography { get; set; }

    public bool IsAvailable { get; set; }

    public List<int> SpecializationIds { get; set; } = new();
}

// ─── Appointment DTOs ─────────────────────────────────────────────────────────

public class AppointmentDto
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public string PatientName { get; set; } = string.Empty;
    public int DoctorId { get; set; }
    public string DoctorName { get; set; } = string.Empty;
    public DateTime AppointmentDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? Notes { get; set; }
    public string? Symptoms { get; set; }
    public decimal? Fee { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateAppointmentDto
{
    [Required]
    public int DoctorId { get; set; }

    [Required]
    public DateTime AppointmentDate { get; set; }

    [MaxLength(500)]
    public string? Symptoms { get; set; }

    [MaxLength(500)]
    public string? Notes { get; set; }
}

public class UpdateAppointmentDto
{
    public DateTime? AppointmentDate { get; set; }

    [MaxLength(20)]
    public string? Status { get; set; }

    [MaxLength(500)]
    public string? Notes { get; set; }
}

public class PatchAppointmentDto
{
    public string? Status { get; set; }
    public string? Notes { get; set; }
}

// ─── Specialization DTOs ──────────────────────────────────────────────────────

public class SpecializationDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int DoctorCount { get; set; }
}

public class CreateSpecializationDto
{
    [Required, MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(300)]
    public string? Description { get; set; }
}

// ─── Prescription DTOs ────────────────────────────────────────────────────────

public class PrescriptionDto
{
    public int Id { get; set; }
    public int AppointmentId { get; set; }
    public string DoctorName { get; set; } = string.Empty;
    public string PatientName { get; set; } = string.Empty;
    public string? Diagnosis { get; set; }
    public string? Instructions { get; set; }
    public DateTime IssuedDate { get; set; }
    public List<PrescriptionMedicineDto> Medicines { get; set; } = new();
}

public class PrescriptionMedicineDto
{
    public string MedicineName { get; set; } = string.Empty;
    public string? Dosage { get; set; }
    public string? Frequency { get; set; }
    public string? Duration { get; set; }
}

public class CreatePrescriptionDto
{
    [Required]
    public int AppointmentId { get; set; }

    [MaxLength(1000)]
    public string? Diagnosis { get; set; }

    [MaxLength(500)]
    public string? Instructions { get; set; }

    public DateTime? ValidUntil { get; set; }

    public List<AddMedicineDto> Medicines { get; set; } = new();
}

public class AddMedicineDto
{
    [Required]
    public int MedicineId { get; set; }
    public string? Dosage { get; set; }
    public string? Frequency { get; set; }
    public string? Duration { get; set; }
}

// ─── Department DTOs ──────────────────────────────────────────────────────────

public class DepartmentDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int DoctorCount { get; set; }
}

public class CreateDepartmentDto
{
    [Required, MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(300)]
    public string? Description { get; set; }
}

public class UpdateDepartmentDto
{
    [Required, MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(300)]
    public string? Description { get; set; }
}

// ─── Bill DTOs ────────────────────────────────────────────────────────────────

public class BillDto
{
    public int Id { get; set; }
    public int AppointmentId { get; set; }
    public string PatientName { get; set; } = string.Empty;
    public string DoctorName { get; set; } = string.Empty;
    public decimal ConsultationFee { get; set; }
    public decimal MedicineCharges { get; set; }
    public decimal TotalAmount { get; set; }
    public string PaymentStatus { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? PaidAt { get; set; }
}

public class CreateBillDto
{
    [Required]
    public int AppointmentId { get; set; }

    [Range(0, 100000)]
    public decimal ConsultationFee { get; set; }

    [Range(0, 100000)]
    public decimal MedicineCharges { get; set; }
}

public class UpdateBillDto
{
    [Range(0, 100000)]
    public decimal? ConsultationFee { get; set; }

    [Range(0, 100000)]
    public decimal? MedicineCharges { get; set; }

    [MaxLength(20)]
    public string? PaymentStatus { get; set; }
}

// ─── Pagination ───────────────────────────────────────────────────────────────

public class PagedResult<T>
{
    public List<T> Items { get; set; } = new();
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    public bool HasPreviousPage => Page > 1;
    public bool HasNextPage => Page < TotalPages;
}

public class QueryParameters
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? Search { get; set; }
    public string? SortBy { get; set; }
    public bool SortDescending { get; set; }
}

// ─── Error Response ───────────────────────────────────────────────────────────

public class ErrorResponse
{
    public string Message { get; set; } = string.Empty;
    public int StatusCode { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string? Details { get; set; }
}

// ─── Dashboard ────────────────────────────────────────────────────────────────

public class DashboardDto
{
    public int TotalPatients { get; set; }
    public int TotalDoctors { get; set; }
    public int TotalAppointments { get; set; }
    public int PendingAppointments { get; set; }
    public int TodayAppointments { get; set; }
    public int CompletedAppointments { get; set; }
}
