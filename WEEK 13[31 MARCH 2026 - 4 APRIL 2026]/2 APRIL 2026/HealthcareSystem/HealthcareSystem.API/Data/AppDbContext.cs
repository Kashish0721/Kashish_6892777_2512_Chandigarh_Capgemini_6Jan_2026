using HealthcareSystem.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace HealthcareSystem.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Doctor> Doctors => Set<Doctor>();
    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<Appointment> Appointments => Set<Appointment>();
    public DbSet<Bill> Bills => Set<Bill>();
    public DbSet<Specialization> Specializations => Set<Specialization>();
    public DbSet<DoctorSpecialization> DoctorSpecializations => Set<DoctorSpecialization>();
    public DbSet<Prescription> Prescriptions => Set<Prescription>();
    public DbSet<Medicine> Medicines => Set<Medicine>();
    public DbSet<PrescriptionMedicine> PrescriptionMedicines => Set<PrescriptionMedicine>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ── Department ────────────────────────────────────────────────────────
        modelBuilder.Entity<Department>(e =>
        {
            e.HasKey(d => d.Id);
            e.Property(d => d.Name).HasMaxLength(100).IsRequired();
            e.Property(d => d.Description).HasMaxLength(300);
        });

        // ── User ──────────────────────────────────────────────────────────────
        modelBuilder.Entity<User>(e =>
        {
            e.HasKey(u => u.Id);
            e.HasIndex(u => u.Email).IsUnique();
            e.Property(u => u.Email).HasMaxLength(200).IsRequired();
            e.Property(u => u.FullName).HasMaxLength(100).IsRequired();
            e.Property(u => u.Role).HasMaxLength(20).HasDefaultValue("Patient");
        });

        // ── One-to-One: User → Patient ────────────────────────────────────────
        modelBuilder.Entity<Patient>(e =>
        {
            e.HasKey(p => p.Id);
            e.HasOne(p => p.User)
             .WithOne(u => u.Patient)
             .HasForeignKey<Patient>(p => p.UserId)
             .OnDelete(DeleteBehavior.Cascade);
        });

        // ── One-to-One: User → Doctor ─────────────────────────────────────────
        modelBuilder.Entity<Doctor>(e =>
        {
            e.HasKey(d => d.Id);
            e.Property(d => d.ConsultationFee).HasColumnType("decimal(10,2)");
            e.HasOne(d => d.User)
             .WithOne(u => u.Doctor)
             .HasForeignKey<Doctor>(d => d.UserId)
             .OnDelete(DeleteBehavior.Cascade);

            // One-to-Many: Department → Doctors
            e.HasOne(d => d.Department)
             .WithMany(dep => dep.Doctors)
             .HasForeignKey(d => d.DepartmentId)
             .OnDelete(DeleteBehavior.SetNull);
        });

        // ── One-to-Many: Doctor → Appointments ────────────────────────────────
        modelBuilder.Entity<Appointment>(e =>
        {
            e.HasKey(a => a.Id);
            e.Property(a => a.Fee).HasColumnType("decimal(10,2)");
            e.HasOne(a => a.Doctor)
             .WithMany(d => d.Appointments)
             .HasForeignKey(a => a.DoctorId)
             .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(a => a.Patient)
             .WithMany(p => p.Appointments)
             .HasForeignKey(a => a.PatientId)
             .OnDelete(DeleteBehavior.Restrict);
        });

        // ── One-to-One: Appointment → Bill ────────────────────────────────────
        modelBuilder.Entity<Bill>(e =>
        {
            e.HasKey(b => b.Id);
            e.Property(b => b.ConsultationFee).HasColumnType("decimal(10,2)");
            e.Property(b => b.MedicineCharges).HasColumnType("decimal(10,2)");
            e.Property(b => b.TotalAmount).HasColumnType("decimal(10,2)");
            e.HasOne(b => b.Appointment)
             .WithOne(a => a.Bill)
             .HasForeignKey<Bill>(b => b.AppointmentId)
             .OnDelete(DeleteBehavior.Cascade);
        });

        // ── Many-to-Many: Doctor ↔ Specialization ────────────────────────────
        modelBuilder.Entity<DoctorSpecialization>(e =>
        {
            e.HasKey(ds => new { ds.DoctorId, ds.SpecializationId });
            e.HasOne(ds => ds.Doctor)
             .WithMany(d => d.DoctorSpecializations)
             .HasForeignKey(ds => ds.DoctorId);
            e.HasOne(ds => ds.Specialization)
             .WithMany(s => s.DoctorSpecializations)
             .HasForeignKey(ds => ds.SpecializationId);
        });

        // ── Prescription ──────────────────────────────────────────────────────
        modelBuilder.Entity<Prescription>(e =>
        {
            e.HasKey(p => p.Id);
            e.HasOne(p => p.Appointment)
             .WithOne(a => a.Prescription)
             .HasForeignKey<Prescription>(p => p.AppointmentId)
             .OnDelete(DeleteBehavior.Cascade);
            e.HasOne(p => p.Doctor)
             .WithMany(d => d.Prescriptions)
             .HasForeignKey(p => p.DoctorId)
             .OnDelete(DeleteBehavior.Restrict);
        });

        // ── PrescriptionMedicine (join) ────────────────────────────────────────
        modelBuilder.Entity<PrescriptionMedicine>(e =>
        {
            e.HasKey(pm => new { pm.PrescriptionId, pm.MedicineId });
            e.HasOne(pm => pm.Prescription)
             .WithMany(p => p.PrescriptionMedicines)
             .HasForeignKey(pm => pm.PrescriptionId);
            e.HasOne(pm => pm.Medicine)
             .WithMany(m => m.PrescriptionMedicines)
             .HasForeignKey(pm => pm.MedicineId);
        });

        // ── Seed data ─────────────────────────────────────────────────────────
        SeedData(modelBuilder);
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        // Departments
        modelBuilder.Entity<Department>().HasData(
            new Department { Id = 1, Name = "Cardiology", Description = "Heart and cardiovascular system" },
            new Department { Id = 2, Name = "Neurology", Description = "Brain and nervous system" },
            new Department { Id = 3, Name = "Orthopedics", Description = "Bones and joints" },
            new Department { Id = 4, Name = "Pediatrics", Description = "Children's health" },
            new Department { Id = 5, Name = "Dermatology", Description = "Skin conditions" },
            new Department { Id = 6, Name = "General Medicine", Description = "General health care" }
        );

        modelBuilder.Entity<Specialization>().HasData(
            new Specialization { Id = 1, Name = "Cardiology", Description = "Heart and cardiovascular system" },
            new Specialization { Id = 2, Name = "Neurology", Description = "Brain and nervous system" },
            new Specialization { Id = 3, Name = "Orthopedics", Description = "Bones and joints" },
            new Specialization { Id = 4, Name = "Pediatrics", Description = "Children's health" },
            new Specialization { Id = 5, Name = "Dermatology", Description = "Skin conditions" },
            new Specialization { Id = 6, Name = "General Medicine", Description = "General health care" }
        );

        modelBuilder.Entity<Medicine>().HasData(
            new Medicine { Id = 1, Name = "Paracetamol", Generic = "Acetaminophen", Dosage = "500mg", Description = "Pain reliever and fever reducer" },
            new Medicine { Id = 2, Name = "Amoxicillin", Generic = "Amoxicillin", Dosage = "250mg", Description = "Antibiotic" },
            new Medicine { Id = 3, Name = "Ibuprofen", Generic = "Ibuprofen", Dosage = "400mg", Description = "Anti-inflammatory" },
            new Medicine { Id = 4, Name = "Metformin", Generic = "Metformin HCl", Dosage = "500mg", Description = "Diabetes medication" },
            new Medicine { Id = 5, Name = "Atorvastatin", Generic = "Atorvastatin", Dosage = "10mg", Description = "Cholesterol lowering" }
        );

        // Admin user (password: Admin@123)
        modelBuilder.Entity<User>().HasData(new User
        {
            Id = 1,
            FullName = "System Admin",
            Email = "admin@healthcare.com",
            PasswordHash = "$2a$11$examplefixedhashstring1234567890",
            Role = "Admin",
            IsActive = true,
            CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc)
        });
    }
}
