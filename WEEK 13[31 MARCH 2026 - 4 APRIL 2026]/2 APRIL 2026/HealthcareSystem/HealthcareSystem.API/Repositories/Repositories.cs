using HealthcareSystem.API.Data;
using HealthcareSystem.API.Repositories.Interfaces;
using HealthcareSystem.Models.DTOs;
using HealthcareSystem.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace HealthcareSystem.API.Repositories;

// ─── Patient Repository ───────────────────────────────────────────────────────
public class PatientRepository : GenericRepository<Patient>, IPatientRepository
{
    public PatientRepository(AppDbContext context) : base(context) { }

    public async Task<Patient?> GetByUserIdAsync(int userId) =>
        await _context.Patients.Include(p => p.User).FirstOrDefaultAsync(p => p.UserId == userId);

    public async Task<Patient?> GetWithDetailsAsync(int id) =>
        await _context.Patients
            .Include(p => p.User)
            .Include(p => p.Appointments).ThenInclude(a => a.Doctor).ThenInclude(d => d.User)
            .FirstOrDefaultAsync(p => p.Id == id);

    public async Task<PagedResult<Patient>> GetPagedAsync(QueryParameters parameters)
    {
        var query = _context.Patients.Include(p => p.User).AsQueryable();

        if (!string.IsNullOrWhiteSpace(parameters.Search))
            query = query.Where(p => p.User.FullName.Contains(parameters.Search) ||
                                     p.User.Email.Contains(parameters.Search));

        var total = await query.CountAsync();
        var items = await query
            .OrderByDescending(p => p.CreatedAt)
            .Skip((parameters.Page - 1) * parameters.PageSize)
            .Take(parameters.PageSize)
            .ToListAsync();

        return new PagedResult<Patient> { Items = items, TotalCount = total, Page = parameters.Page, PageSize = parameters.PageSize };
    }
}

// ─── Doctor Repository ────────────────────────────────────────────────────────
public class DoctorRepository : GenericRepository<Doctor>, IDoctorRepository
{
    public DoctorRepository(AppDbContext context) : base(context) { }

    public async Task<Doctor?> GetByUserIdAsync(int userId) =>
        await _context.Doctors.Include(d => d.User)
            .Include(d => d.DoctorSpecializations).ThenInclude(ds => ds.Specialization)
            .FirstOrDefaultAsync(d => d.UserId == userId);

    public async Task<Doctor?> GetWithDetailsAsync(int id) =>
        await _context.Doctors
            .Include(d => d.User)
            .Include(d => d.DoctorSpecializations).ThenInclude(ds => ds.Specialization)
            .Include(d => d.Appointments).ThenInclude(a => a.Patient).ThenInclude(p => p.User)
            .FirstOrDefaultAsync(d => d.Id == id);

    public async Task<PagedResult<Doctor>> GetPagedAsync(QueryParameters parameters)
    {
        var query = _context.Doctors
            .Include(d => d.User)
            .Include(d => d.DoctorSpecializations).ThenInclude(ds => ds.Specialization)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(parameters.Search))
            query = query.Where(d => d.User.FullName.Contains(parameters.Search) ||
                d.DoctorSpecializations.Any(ds => ds.Specialization.Name.Contains(parameters.Search)));

        var total = await query.CountAsync();
        var items = await query
            .OrderByDescending(d => d.CreatedAt)
            .Skip((parameters.Page - 1) * parameters.PageSize)
            .Take(parameters.PageSize)
            .ToListAsync();

        return new PagedResult<Doctor> { Items = items, TotalCount = total, Page = parameters.Page, PageSize = parameters.PageSize };
    }

    public async Task<IEnumerable<Doctor>> GetBySpecializationAsync(int specializationId) =>
        await _context.Doctors
            .Include(d => d.User)
            .Include(d => d.DoctorSpecializations).ThenInclude(ds => ds.Specialization)
            .Where(d => d.DoctorSpecializations.Any(ds => ds.SpecializationId == specializationId) && d.IsAvailable)
            .ToListAsync();

    public async Task UpdateSpecializationsAsync(int doctorId, List<int> specializationIds)
    {
        var existing = await _context.DoctorSpecializations
            .Where(ds => ds.DoctorId == doctorId).ToListAsync();
        _context.DoctorSpecializations.RemoveRange(existing);

        var newEntries = specializationIds.Select(sid => new DoctorSpecialization
        { DoctorId = doctorId, SpecializationId = sid });
        await _context.DoctorSpecializations.AddRangeAsync(newEntries);
        await _context.SaveChangesAsync();
    }
}

// ─── Appointment Repository ───────────────────────────────────────────────────
public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
{
    public AppointmentRepository(AppDbContext context) : base(context) { }

    public async Task<Appointment?> GetWithDetailsAsync(int id) =>
        await _context.Appointments
            .Include(a => a.Patient).ThenInclude(p => p.User)
            .Include(a => a.Doctor).ThenInclude(d => d.User)
            .Include(a => a.Prescription)
            .FirstOrDefaultAsync(a => a.Id == id);

    public async Task<PagedResult<Appointment>> GetPagedAsync(QueryParameters parameters)
    {
        var query = _context.Appointments
            .Include(a => a.Patient).ThenInclude(p => p.User)
            .Include(a => a.Doctor).ThenInclude(d => d.User)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(parameters.Search))
            query = query.Where(a => a.Patient.User.FullName.Contains(parameters.Search) ||
                                     a.Doctor.User.FullName.Contains(parameters.Search) ||
                                     a.Status.Contains(parameters.Search));

        var total = await query.CountAsync();
        var items = await query
            .OrderByDescending(a => a.AppointmentDate)
            .Skip((parameters.Page - 1) * parameters.PageSize)
            .Take(parameters.PageSize)
            .ToListAsync();

        return new PagedResult<Appointment> { Items = items, TotalCount = total, Page = parameters.Page, PageSize = parameters.PageSize };
    }

    public async Task<IEnumerable<Appointment>> GetByPatientAsync(int patientId) =>
        await _context.Appointments
            .Include(a => a.Doctor).ThenInclude(d => d.User)
            .Where(a => a.PatientId == patientId)
            .OrderByDescending(a => a.AppointmentDate).ToListAsync();

    public async Task<IEnumerable<Appointment>> GetByDoctorAsync(int doctorId) =>
        await _context.Appointments
            .Include(a => a.Patient).ThenInclude(p => p.User)
            .Where(a => a.DoctorId == doctorId)
            .OrderByDescending(a => a.AppointmentDate).ToListAsync();

    public async Task<IEnumerable<Appointment>> GetByDateAsync(DateTime date) =>
        await _context.Appointments
            .Include(a => a.Patient).ThenInclude(p => p.User)
            .Include(a => a.Doctor).ThenInclude(d => d.User)
            .Where(a => a.AppointmentDate.Date == date.Date)
            .ToListAsync();

    public async Task<int> GetTodayCountAsync() =>
        await _context.Appointments.CountAsync(a => a.AppointmentDate.Date == DateTime.UtcNow.Date);
}

// ─── Specialization Repository ────────────────────────────────────────────────
public class SpecializationRepository : GenericRepository<Specialization>, ISpecializationRepository
{
    public SpecializationRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<Specialization>> GetAllWithCountAsync() =>
        await _context.Specializations
            .Include(s => s.DoctorSpecializations)
            .ToListAsync();
}

// ─── Prescription Repository ──────────────────────────────────────────────────
public class PrescriptionRepository : GenericRepository<Prescription>, IPrescriptionRepository
{
    public PrescriptionRepository(AppDbContext context) : base(context) { }

    public async Task<Prescription?> GetWithDetailsAsync(int id) =>
        await _context.Prescriptions
            .Include(p => p.Doctor).ThenInclude(d => d.User)
            .Include(p => p.Appointment).ThenInclude(a => a.Patient).ThenInclude(pt => pt.User)
            .Include(p => p.PrescriptionMedicines).ThenInclude(pm => pm.Medicine)
            .FirstOrDefaultAsync(p => p.Id == id);

    public async Task<IEnumerable<Prescription>> GetByPatientAsync(int patientId) =>
        await _context.Prescriptions
            .Include(p => p.Doctor).ThenInclude(d => d.User)
            .Include(p => p.PrescriptionMedicines).ThenInclude(pm => pm.Medicine)
            .Where(p => p.Appointment.PatientId == patientId)
            .OrderByDescending(p => p.IssuedDate).ToListAsync();

    public async Task<IEnumerable<Prescription>> GetByDoctorAsync(int doctorId) =>
        await _context.Prescriptions
            .Include(p => p.Appointment).ThenInclude(a => a.Patient).ThenInclude(pt => pt.User)
            .Include(p => p.PrescriptionMedicines).ThenInclude(pm => pm.Medicine)
            .Where(p => p.DoctorId == doctorId)
            .OrderByDescending(p => p.IssuedDate).ToListAsync();
}

// ─── Department Repository ────────────────────────────────────────────────────
public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
{
    public DepartmentRepository(AppDbContext context) : base(context) { }

    public async Task<Department?> GetWithDetailsAsync(int id) =>
        await _context.Departments
            .Include(d => d.Doctors)
            .FirstOrDefaultAsync(d => d.Id == id);

    public async Task<IEnumerable<Department>> GetAllWithDoctorCountAsync() =>
        await _context.Departments
            .Include(d => d.Doctors)
            .ToListAsync();
}

// ─── Bill Repository ──────────────────────────────────────────────────────────
public class BillRepository : GenericRepository<Bill>, IBillRepository
{
    public BillRepository(AppDbContext context) : base(context) { }

    public async Task<Bill?> GetWithDetailsAsync(int id) =>
        await _context.Bills
            .Include(b => b.Appointment).ThenInclude(a => a.Patient).ThenInclude(p => p.User)
            .Include(b => b.Appointment).ThenInclude(a => a.Doctor).ThenInclude(d => d.User)
            .FirstOrDefaultAsync(b => b.Id == id);

    public async Task<Bill?> GetByAppointmentAsync(int appointmentId) =>
        await _context.Bills
            .Include(b => b.Appointment).ThenInclude(a => a.Patient).ThenInclude(p => p.User)
            .Include(b => b.Appointment).ThenInclude(a => a.Doctor).ThenInclude(d => d.User)
            .FirstOrDefaultAsync(b => b.AppointmentId == appointmentId);

    public async Task<IEnumerable<Bill>> GetByPatientAsync(int patientId) =>
        await _context.Bills
            .Include(b => b.Appointment).ThenInclude(a => a.Doctor).ThenInclude(d => d.User)
            .Where(b => b.Appointment.PatientId == patientId)
            .OrderByDescending(b => b.CreatedAt)
            .ToListAsync();

    public async Task<IEnumerable<Bill>> GetAllWithDetailsAsync() =>
        await _context.Bills
            .Include(b => b.Appointment).ThenInclude(a => a.Patient).ThenInclude(p => p.User)
            .Include(b => b.Appointment).ThenInclude(a => a.Doctor).ThenInclude(d => d.User)
            .OrderByDescending(b => b.CreatedAt)
            .ToListAsync();
}
