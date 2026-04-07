using HealthcareSystem.Models.DTOs;
using HealthcareSystem.Models.Entities;

namespace HealthcareSystem.API.Repositories.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<T?> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<bool> ExistsAsync(int id);
}

public interface IUserRepository : IGenericRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
    Task<PagedResult<User>> GetPagedAsync(QueryParameters parameters);
    Task<bool> EmailExistsAsync(string email);
}

public interface IPatientRepository : IGenericRepository<Patient>
{
    Task<Patient?> GetByUserIdAsync(int userId);
    Task<Patient?> GetWithDetailsAsync(int id);
    Task<PagedResult<Patient>> GetPagedAsync(QueryParameters parameters);
}

public interface IDoctorRepository : IGenericRepository<Doctor>
{
    Task<Doctor?> GetByUserIdAsync(int userId);
    Task<Doctor?> GetWithDetailsAsync(int id);
    Task<PagedResult<Doctor>> GetPagedAsync(QueryParameters parameters);
    Task<IEnumerable<Doctor>> GetBySpecializationAsync(int specializationId);
    Task UpdateSpecializationsAsync(int doctorId, List<int> specializationIds);
}

public interface IAppointmentRepository : IGenericRepository<Appointment>
{
    Task<Appointment?> GetWithDetailsAsync(int id);
    Task<PagedResult<Appointment>> GetPagedAsync(QueryParameters parameters);
    Task<IEnumerable<Appointment>> GetByPatientAsync(int patientId);
    Task<IEnumerable<Appointment>> GetByDoctorAsync(int doctorId);
    Task<IEnumerable<Appointment>> GetByDateAsync(DateTime date);
    Task<int> GetTodayCountAsync();
}

public interface ISpecializationRepository : IGenericRepository<Specialization>
{
    Task<IEnumerable<Specialization>> GetAllWithCountAsync();
}

public interface IPrescriptionRepository : IGenericRepository<Prescription>
{
    Task<Prescription?> GetWithDetailsAsync(int id);
    Task<IEnumerable<Prescription>> GetByPatientAsync(int patientId);
    Task<IEnumerable<Prescription>> GetByDoctorAsync(int doctorId);
}

public interface IDepartmentRepository : IGenericRepository<Department>
{
    Task<Department?> GetWithDetailsAsync(int id);
    Task<IEnumerable<Department>> GetAllWithDoctorCountAsync();
}

public interface IBillRepository : IGenericRepository<Bill>
{
    Task<Bill?> GetWithDetailsAsync(int id);
    Task<Bill?> GetByAppointmentAsync(int appointmentId);
    Task<IEnumerable<Bill>> GetByPatientAsync(int patientId);
    Task<IEnumerable<Bill>> GetAllWithDetailsAsync();
}
