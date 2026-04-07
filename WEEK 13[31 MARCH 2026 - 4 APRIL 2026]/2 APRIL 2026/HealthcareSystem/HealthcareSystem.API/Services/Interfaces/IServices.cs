using HealthcareSystem.Models.DTOs;

namespace HealthcareSystem.API.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto?> LoginAsync(LoginDto dto);
    Task<AuthResponseDto?> RegisterAsync(RegisterDto dto);
    Task<AuthResponseDto?> RefreshTokenAsync(RefreshTokenDto dto);
}

public interface IPatientService
{
    Task<PagedResult<PatientDto>> GetAllAsync(QueryParameters parameters);
    Task<PatientDto?> GetByIdAsync(int id);
    Task<PatientDto?> GetByUserIdAsync(int userId);
    Task<PatientDto> CreateAsync(int userId, CreatePatientDto dto);
    Task<PatientDto?> UpdateAsync(int id, UpdatePatientDto dto);
    Task<bool> DeleteAsync(int id);
}

public interface IDoctorService
{
    Task<PagedResult<DoctorDto>> GetAllAsync(QueryParameters parameters);
    Task<DoctorDto?> GetByIdAsync(int id);
    Task<DoctorDto?> GetByUserIdAsync(int userId);
    Task<IEnumerable<DoctorDto>> GetBySpecializationAsync(int specializationId);
    Task<DoctorDto> CreateAsync(int userId, CreateDoctorDto dto);
    Task<DoctorDto?> UpdateAsync(int id, UpdateDoctorDto dto);
    Task<bool> DeleteAsync(int id);
}

public interface IAppointmentService
{
    Task<PagedResult<AppointmentDto>> GetAllAsync(QueryParameters parameters);
    Task<AppointmentDto?> GetByIdAsync(int id);
    Task<IEnumerable<AppointmentDto>> GetByPatientAsync(int patientId);
    Task<IEnumerable<AppointmentDto>> GetByDoctorAsync(int doctorId);
    Task<IEnumerable<AppointmentDto>> GetByDateAsync(DateTime date);
    Task<AppointmentDto> CreateAsync(int patientId, CreateAppointmentDto dto);
    Task<AppointmentDto?> UpdateAsync(int id, UpdateAppointmentDto dto);
    Task<AppointmentDto?> PatchAsync(int id, PatchAppointmentDto dto);
    Task<bool> DeleteAsync(int id);
}

public interface ISpecializationService
{
    Task<IEnumerable<SpecializationDto>> GetAllAsync();
    Task<SpecializationDto?> GetByIdAsync(int id);
    Task<SpecializationDto> CreateAsync(CreateSpecializationDto dto);
    Task<bool> DeleteAsync(int id);
}

public interface IPrescriptionService
{
    Task<PrescriptionDto?> GetByIdAsync(int id);
    Task<IEnumerable<PrescriptionDto>> GetByPatientAsync(int patientId);
    Task<IEnumerable<PrescriptionDto>> GetByDoctorAsync(int doctorId);
    Task<PrescriptionDto> CreateAsync(int doctorId, CreatePrescriptionDto dto);
}

public interface IDashboardService
{
    Task<DashboardDto> GetDashboardAsync();
}

public interface IDepartmentService
{
    Task<IEnumerable<DepartmentDto>> GetAllAsync();
    Task<DepartmentDto?> GetByIdAsync(int id);
    Task<DepartmentDto> CreateAsync(CreateDepartmentDto dto);
    Task<DepartmentDto?> UpdateAsync(int id, UpdateDepartmentDto dto);
    Task<bool> DeleteAsync(int id);
}

public interface IBillService
{
    Task<IEnumerable<BillDto>> GetAllAsync();
    Task<BillDto?> GetByIdAsync(int id);
    Task<BillDto?> GetByAppointmentAsync(int appointmentId);
    Task<IEnumerable<BillDto>> GetByPatientAsync(int patientId);
    Task<BillDto> CreateAsync(CreateBillDto dto);
    Task<BillDto?> UpdateAsync(int id, UpdateBillDto dto);
    Task<BillDto?> MarkAsPaidAsync(int id);
    Task<bool> DeleteAsync(int id);
}
