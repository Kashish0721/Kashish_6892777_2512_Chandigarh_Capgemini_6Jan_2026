using AutoMapper;
using HealthcareSystem.API.Data;
using HealthcareSystem.API.Repositories.Interfaces;
using HealthcareSystem.API.Services.Interfaces;
using HealthcareSystem.Models.DTOs;
using HealthcareSystem.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace HealthcareSystem.API.Services;

// ─── Patient Service ──────────────────────────────────────────────────────────
public class PatientService : IPatientService
{
    private readonly IPatientRepository _repo;
    private readonly IMapper _mapper;
    private readonly ILogger<PatientService> _logger;

    public PatientService(IPatientRepository repo, IMapper mapper, ILogger<PatientService> logger)
    { _repo = repo; _mapper = mapper; _logger = logger; }

    public async Task<PagedResult<PatientDto>> GetAllAsync(QueryParameters p)
    {
        var paged = await _repo.GetPagedAsync(p);
        return new PagedResult<PatientDto>
        {
            Items = _mapper.Map<List<PatientDto>>(paged.Items),
            TotalCount = paged.TotalCount, Page = paged.Page, PageSize = paged.PageSize
        };
    }

    public async Task<PatientDto?> GetByIdAsync(int id)
    {
        var entity = await _repo.GetWithDetailsAsync(id);
        return entity == null ? null : _mapper.Map<PatientDto>(entity);
    }

    public async Task<PatientDto?> GetByUserIdAsync(int userId)
    {
        var entity = await _repo.GetByUserIdAsync(userId);
        return entity == null ? null : _mapper.Map<PatientDto>(entity);
    }

    public async Task<PatientDto> CreateAsync(int userId, CreatePatientDto dto)
    {
        var entity = _mapper.Map<Patient>(dto);
        entity.UserId = userId;
        await _repo.AddAsync(entity);
        var created = await _repo.GetWithDetailsAsync(entity.Id);
        _logger.LogInformation("Patient profile created for UserId={UserId}", userId);
        return _mapper.Map<PatientDto>(created!);
    }

    public async Task<PatientDto?> UpdateAsync(int id, UpdatePatientDto dto)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity == null) return null;
        _mapper.Map(dto, entity);
        await _repo.UpdateAsync(entity);
        var updated = await _repo.GetWithDetailsAsync(id);
        return _mapper.Map<PatientDto>(updated!);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity == null) return false;
        await _repo.DeleteAsync(entity);
        return true;
    }
}

// ─── Doctor Service ───────────────────────────────────────────────────────────
public class DoctorService : IDoctorService
{
    private readonly IDoctorRepository _repo;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _cache;
    private readonly ILogger<DoctorService> _logger;
    private const string DoctorsCacheKey = "doctors_all";

    public DoctorService(IDoctorRepository repo, IMapper mapper, IMemoryCache cache, ILogger<DoctorService> logger)
    { _repo = repo; _mapper = mapper; _cache = cache; _logger = logger; }

    public async Task<PagedResult<DoctorDto>> GetAllAsync(QueryParameters p)
    {
        var paged = await _repo.GetPagedAsync(p);
        return new PagedResult<DoctorDto>
        {
            Items = _mapper.Map<List<DoctorDto>>(paged.Items),
            TotalCount = paged.TotalCount, Page = paged.Page, PageSize = paged.PageSize
        };
    }

    public async Task<DoctorDto?> GetByIdAsync(int id)
    {
        var entity = await _repo.GetWithDetailsAsync(id);
        return entity == null ? null : _mapper.Map<DoctorDto>(entity);
    }

    public async Task<DoctorDto?> GetByUserIdAsync(int userId)
    {
        var entity = await _repo.GetByUserIdAsync(userId);
        return entity == null ? null : _mapper.Map<DoctorDto>(entity);
    }

    public async Task<IEnumerable<DoctorDto>> GetBySpecializationAsync(int specializationId)
    {
        // Cache specialization-based doctor lists for 10 minutes
        var cacheKey = $"doctors_spec_{specializationId}";
        if (!_cache.TryGetValue(cacheKey, out IEnumerable<DoctorDto>? cached))
        {
            var doctors = await _repo.GetBySpecializationAsync(specializationId);
            cached = _mapper.Map<IEnumerable<DoctorDto>>(doctors);
            _cache.Set(cacheKey, cached, TimeSpan.FromMinutes(10));
            _logger.LogInformation("Doctor list for specialization {Id} cached", specializationId);
        }
        return cached!;
    }

    public async Task<DoctorDto> CreateAsync(int userId, CreateDoctorDto dto)
    {
        var entity = _mapper.Map<Doctor>(dto);
        entity.UserId = userId;
        await _repo.AddAsync(entity);

        if (dto.SpecializationIds.Any())
            await _repo.UpdateSpecializationsAsync(entity.Id, dto.SpecializationIds);

        _cache.Remove(DoctorsCacheKey);
        var created = await _repo.GetWithDetailsAsync(entity.Id);
        _logger.LogInformation("Doctor profile created for UserId={UserId}", userId);
        return _mapper.Map<DoctorDto>(created!);
    }

    public async Task<DoctorDto?> UpdateAsync(int id, UpdateDoctorDto dto)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity == null) return null;
        _mapper.Map(dto, entity);
        await _repo.UpdateAsync(entity);

        if (dto.SpecializationIds.Any())
            await _repo.UpdateSpecializationsAsync(id, dto.SpecializationIds);

        _cache.Remove(DoctorsCacheKey);
        var updated = await _repo.GetWithDetailsAsync(id);
        return _mapper.Map<DoctorDto>(updated!);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity == null) return false;
        await _repo.DeleteAsync(entity);
        _cache.Remove(DoctorsCacheKey);
        return true;
    }
}

// ─── Appointment Service ──────────────────────────────────────────────────────
public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _repo;
    private readonly IPatientRepository _patientRepo;
    private readonly IDoctorRepository _doctorRepo;
    private readonly IMapper _mapper;
    private readonly ILogger<AppointmentService> _logger;

    public AppointmentService(IAppointmentRepository repo, IPatientRepository patientRepo,
        IDoctorRepository doctorRepo, IMapper mapper, ILogger<AppointmentService> logger)
    { _repo = repo; _patientRepo = patientRepo; _doctorRepo = doctorRepo; _mapper = mapper; _logger = logger; }

    public async Task<PagedResult<AppointmentDto>> GetAllAsync(QueryParameters p)
    {
        var paged = await _repo.GetPagedAsync(p);
        return new PagedResult<AppointmentDto>
        {
            Items = _mapper.Map<List<AppointmentDto>>(paged.Items),
            TotalCount = paged.TotalCount, Page = paged.Page, PageSize = paged.PageSize
        };
    }

    public async Task<AppointmentDto?> GetByIdAsync(int id)
    {
        var entity = await _repo.GetWithDetailsAsync(id);
        return entity == null ? null : _mapper.Map<AppointmentDto>(entity);
    }

    public async Task<IEnumerable<AppointmentDto>> GetByPatientAsync(int patientId)
        => _mapper.Map<IEnumerable<AppointmentDto>>(await _repo.GetByPatientAsync(patientId));

    public async Task<IEnumerable<AppointmentDto>> GetByDoctorAsync(int doctorId)
        => _mapper.Map<IEnumerable<AppointmentDto>>(await _repo.GetByDoctorAsync(doctorId));

    public async Task<IEnumerable<AppointmentDto>> GetByDateAsync(DateTime date)
        => _mapper.Map<IEnumerable<AppointmentDto>>(await _repo.GetByDateAsync(date));

    public async Task<AppointmentDto> CreateAsync(int patientId, CreateAppointmentDto dto)
    {
        var doctor = await _doctorRepo.GetByIdAsync(dto.DoctorId)
            ?? throw new KeyNotFoundException("Doctor not found");

        var entity = _mapper.Map<Appointment>(dto);
        entity.PatientId = patientId;
        entity.Fee = doctor.ConsultationFee;

        await _repo.AddAsync(entity);
        var created = await _repo.GetWithDetailsAsync(entity.Id);
        _logger.LogInformation("Appointment booked: Patient={PatientId} Doctor={DoctorId} Date={Date}",
            patientId, dto.DoctorId, dto.AppointmentDate);
        return _mapper.Map<AppointmentDto>(created!);
    }

    public async Task<AppointmentDto?> UpdateAsync(int id, UpdateAppointmentDto dto)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity == null) return null;
        _mapper.Map(dto, entity);
        entity.UpdatedAt = DateTime.UtcNow;
        await _repo.UpdateAsync(entity);
        var updated = await _repo.GetWithDetailsAsync(id);
        return _mapper.Map<AppointmentDto>(updated!);
    }

    public async Task<AppointmentDto?> PatchAsync(int id, PatchAppointmentDto dto)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity == null) return null;
        if (dto.Status != null) entity.Status = dto.Status;
        if (dto.Notes != null) entity.Notes = dto.Notes;
        entity.UpdatedAt = DateTime.UtcNow;
        await _repo.UpdateAsync(entity);
        var updated = await _repo.GetWithDetailsAsync(id);
        _logger.LogInformation("Appointment {Id} status updated to {Status}", id, dto.Status);
        return _mapper.Map<AppointmentDto>(updated!);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity == null) return false;
        await _repo.DeleteAsync(entity);
        return true;
    }
}

// ─── Specialization Service ───────────────────────────────────────────────────
public class SpecializationService : ISpecializationService
{
    private readonly ISpecializationRepository _repo;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _cache;
    private const string CacheKey = "specializations_all";

    public SpecializationService(ISpecializationRepository repo, IMapper mapper, IMemoryCache cache)
    { _repo = repo; _mapper = mapper; _cache = cache; }

    public async Task<IEnumerable<SpecializationDto>> GetAllAsync()
    {
        if (!_cache.TryGetValue(CacheKey, out IEnumerable<SpecializationDto>? cached))
        {
            var items = await _repo.GetAllWithCountAsync();
            cached = _mapper.Map<IEnumerable<SpecializationDto>>(items);
            _cache.Set(CacheKey, cached, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30),
                SlidingExpiration = TimeSpan.FromMinutes(10)
            });
        }
        return cached!;
    }

    public async Task<SpecializationDto?> GetByIdAsync(int id)
    {
        var entity = await _repo.GetByIdAsync(id);
        return entity == null ? null : _mapper.Map<SpecializationDto>(entity);
    }

    public async Task<SpecializationDto> CreateAsync(CreateSpecializationDto dto)
    {
        var entity = _mapper.Map<Specialization>(dto);
        await _repo.AddAsync(entity);
        _cache.Remove(CacheKey);
        return _mapper.Map<SpecializationDto>(entity);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity == null) return false;
        await _repo.DeleteAsync(entity);
        _cache.Remove(CacheKey);
        return true;
    }
}

// ─── Prescription Service ─────────────────────────────────────────────────────
public class PrescriptionService : IPrescriptionService
{
    private readonly IPrescriptionRepository _repo;
    private readonly IAppointmentRepository _apptRepo;
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<PrescriptionService> _logger;

    public PrescriptionService(IPrescriptionRepository repo, IAppointmentRepository apptRepo,
        AppDbContext context, IMapper mapper, ILogger<PrescriptionService> logger)
    { _repo = repo; _apptRepo = apptRepo; _context = context; _mapper = mapper; _logger = logger; }

    public async Task<PrescriptionDto?> GetByIdAsync(int id)
    {
        var entity = await _repo.GetWithDetailsAsync(id);
        return entity == null ? null : _mapper.Map<PrescriptionDto>(entity);
    }

    public async Task<IEnumerable<PrescriptionDto>> GetByPatientAsync(int patientId)
        => _mapper.Map<IEnumerable<PrescriptionDto>>(await _repo.GetByPatientAsync(patientId));

    public async Task<IEnumerable<PrescriptionDto>> GetByDoctorAsync(int doctorId)
        => _mapper.Map<IEnumerable<PrescriptionDto>>(await _repo.GetByDoctorAsync(doctorId));

    public async Task<PrescriptionDto> CreateAsync(int doctorId, CreatePrescriptionDto dto)
    {
        var appt = await _apptRepo.GetWithDetailsAsync(dto.AppointmentId)
            ?? throw new KeyNotFoundException("Appointment not found");

        var prescription = new Prescription
        {
            AppointmentId = dto.AppointmentId,
            DoctorId = doctorId,
            PatientId = appt.PatientId,
            Diagnosis = dto.Diagnosis,
            Instructions = dto.Instructions,
            ValidUntil = dto.ValidUntil
        };

        await _repo.AddAsync(prescription);

        foreach (var med in dto.Medicines)
        {
            await _context.PrescriptionMedicines.AddAsync(new PrescriptionMedicine
            {
                PrescriptionId = prescription.Id,
                MedicineId = med.MedicineId,
                Dosage = med.Dosage,
                Frequency = med.Frequency,
                Duration = med.Duration
            });
        }
        await _context.SaveChangesAsync();

        // Mark appointment completed
        appt.Status = "Completed";
        appt.UpdatedAt = DateTime.UtcNow;
        await _apptRepo.UpdateAsync(appt);

        _logger.LogInformation("Prescription {Id} issued by Doctor {DoctorId}", prescription.Id, doctorId);
        var created = await _repo.GetWithDetailsAsync(prescription.Id);
        return _mapper.Map<PrescriptionDto>(created!);
    }
}

// ─── Dashboard Service ────────────────────────────────────────────────────────
public class DashboardService : IDashboardService
{
    private readonly AppDbContext _context;

    public DashboardService(AppDbContext context) => _context = context;

    public async Task<DashboardDto> GetDashboardAsync() => new DashboardDto
    {
        TotalPatients = await _context.Patients.CountAsync(),
        TotalDoctors = await _context.Doctors.CountAsync(),
        TotalAppointments = await _context.Appointments.CountAsync(),
        PendingAppointments = await _context.Appointments.CountAsync(a => a.Status == "Pending"),
        TodayAppointments = await _context.Appointments.CountAsync(a => a.AppointmentDate.Date == DateTime.UtcNow.Date),
        CompletedAppointments = await _context.Appointments.CountAsync(a => a.Status == "Completed")
    };
}

// ─── Department Service ───────────────────────────────────────────────────────
public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _repo;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _cache;
    private const string CacheKey = "departments_all";
    private readonly ILogger<DepartmentService> _logger;

    public DepartmentService(IDepartmentRepository repo, IMapper mapper, IMemoryCache cache, ILogger<DepartmentService> logger)
    { _repo = repo; _mapper = mapper; _cache = cache; _logger = logger; }

    public async Task<IEnumerable<DepartmentDto>> GetAllAsync()
    {
        if (!_cache.TryGetValue(CacheKey, out IEnumerable<DepartmentDto>? cached))
        {
            var items = await _repo.GetAllWithDoctorCountAsync();
            cached = _mapper.Map<IEnumerable<DepartmentDto>>(items);
            _cache.Set(CacheKey, cached, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30),
                SlidingExpiration = TimeSpan.FromMinutes(10)
            });
        }
        return cached!;
    }

    public async Task<DepartmentDto?> GetByIdAsync(int id)
    {
        var entity = await _repo.GetWithDetailsAsync(id);
        return entity == null ? null : _mapper.Map<DepartmentDto>(entity);
    }

    public async Task<DepartmentDto> CreateAsync(CreateDepartmentDto dto)
    {
        var entity = _mapper.Map<Department>(dto);
        await _repo.AddAsync(entity);
        _cache.Remove(CacheKey);
        _logger.LogInformation("Department {Name} created with ID {Id}", dto.Name, entity.Id);
        return _mapper.Map<DepartmentDto>(entity);
    }

    public async Task<DepartmentDto?> UpdateAsync(int id, UpdateDepartmentDto dto)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity == null) return null;
        _mapper.Map(dto, entity);
        await _repo.UpdateAsync(entity);
        _cache.Remove(CacheKey);
        _logger.LogInformation("Department {Id} updated", id);
        return _mapper.Map<DepartmentDto>(entity);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity == null) return false;
        await _repo.DeleteAsync(entity);
        _cache.Remove(CacheKey);
        _logger.LogInformation("Department {Id} deleted", id);
        return true;
    }
}

// ─── Bill Service ─────────────────────────────────────────────────────────────
public class BillService : IBillService
{
    private readonly IBillRepository _repo;
    private readonly IAppointmentRepository _apptRepo;
    private readonly IMapper _mapper;
    private readonly ILogger<BillService> _logger;

    public BillService(IBillRepository repo, IAppointmentRepository apptRepo, IMapper mapper, ILogger<BillService> logger)
    { _repo = repo; _apptRepo = apptRepo; _mapper = mapper; _logger = logger; }

    public async Task<IEnumerable<BillDto>> GetAllAsync()
    {
        var bills = await _repo.GetAllWithDetailsAsync();
        return _mapper.Map<IEnumerable<BillDto>>(bills);
    }

    public async Task<BillDto?> GetByIdAsync(int id)
    {
        var entity = await _repo.GetWithDetailsAsync(id);
        return entity == null ? null : _mapper.Map<BillDto>(entity);
    }

    public async Task<BillDto?> GetByAppointmentAsync(int appointmentId)
    {
        var entity = await _repo.GetByAppointmentAsync(appointmentId);
        return entity == null ? null : _mapper.Map<BillDto>(entity);
    }

    public async Task<IEnumerable<BillDto>> GetByPatientAsync(int patientId)
    {
        var bills = await _repo.GetByPatientAsync(patientId);
        return _mapper.Map<IEnumerable<BillDto>>(bills);
    }

    public async Task<BillDto> CreateAsync(CreateBillDto dto)
    {
        _logger.LogInformation("=== BillService.CreateAsync START ===");
        _logger.LogInformation("Request: AppointmentId={AppointmentId}, ConsultationFee={Fee}, MedicineCharges={Charges}", 
            dto.AppointmentId, dto.ConsultationFee, dto.MedicineCharges);
        
        // Log all appointments in database for debugging
        var allAppointments = await _apptRepo.GetAllAsync();
        _logger.LogInformation("Total appointments in DB: {Count}", allAppointments.Count());
        foreach (var a in allAppointments)
        {
            _logger.LogInformation("  Found appointment: ID={Id}, PatientId={PatientId}, DoctorId={DoctorId}, Status={Status}", 
                a.Id, a.PatientId, a.DoctorId, a.Status);
        }
        
        // Now try to get the specific appointment
        _logger.LogInformation("Looking up appointment with ID={AppointmentId}...", dto.AppointmentId);
        var appt = await _apptRepo.GetWithDetailsAsync(dto.AppointmentId);
        
        if (appt == null)
        {
            _logger.LogError("✗ Appointment NOT found with ID={AppointmentId}", dto.AppointmentId);
            throw new KeyNotFoundException($"Appointment not found with ID {dto.AppointmentId}");
        }

        _logger.LogInformation("✓ Found appointment: PatientId={PatientId}, DoctorId={DoctorId}, Status={Status}", 
            appt.PatientId, appt.DoctorId, appt.Status);
        
        // Create bill
        var entity = _mapper.Map<Bill>(dto);
        _logger.LogInformation("Mapped Bill entity, AppointmentId={AppointmentId}", entity.AppointmentId);
        
        await _repo.AddAsync(entity);
        _logger.LogInformation("✓ Bill created successfully, Bill.ID={BillId}", entity.Id);
        _logger.LogInformation("=== BillService.CreateAsync END ===");
        
        return _mapper.Map<BillDto>(entity);
    }

    public async Task<BillDto?> UpdateAsync(int id, UpdateBillDto dto)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity == null) return null;
        _mapper.Map(dto, entity);
        await _repo.UpdateAsync(entity);
        _logger.LogInformation("Bill {Id} updated", id);
        return _mapper.Map<BillDto>(entity);
    }

    public async Task<BillDto?> MarkAsPaidAsync(int id)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity == null) return null;
        entity.PaymentStatus = "Paid";
        entity.PaidAt = DateTime.UtcNow;
        await _repo.UpdateAsync(entity);
        _logger.LogInformation("Bill {Id} marked as paid", id);
        return await GetByIdAsync(id);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity == null) return false;
        await _repo.DeleteAsync(entity);
        _logger.LogInformation("Bill {Id} deleted", id);
        return true;
    }
}
