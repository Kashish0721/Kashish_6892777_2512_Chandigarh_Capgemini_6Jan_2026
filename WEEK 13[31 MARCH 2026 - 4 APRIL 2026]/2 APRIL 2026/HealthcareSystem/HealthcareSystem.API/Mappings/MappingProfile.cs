using AutoMapper;
using HealthcareSystem.Models.DTOs;
using HealthcareSystem.Models.Entities;

namespace HealthcareSystem.API.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // User
        CreateMap<User, UserDto>();
        CreateMap<UpdateUserDto, User>();

        // Department
        CreateMap<Department, DepartmentDto>()
            .ForMember(d => d.DoctorCount, o => o.MapFrom(s => s.Doctors.Count));
        CreateMap<CreateDepartmentDto, Department>();
        CreateMap<UpdateDepartmentDto, Department>();

        // Patient
        CreateMap<Patient, PatientDto>()
            .ForMember(d => d.FullName, o => o.MapFrom(s => s.User.FullName))
            .ForMember(d => d.Email, o => o.MapFrom(s => s.User.Email))
            .ForMember(d => d.TotalAppointments, o => o.MapFrom(s => s.Appointments.Count));

        CreateMap<CreatePatientDto, Patient>();
        CreateMap<UpdatePatientDto, Patient>();

        // Doctor
        CreateMap<Doctor, DoctorDto>()
            .ForMember(d => d.FullName, o => o.MapFrom(s => s.User.FullName))
            .ForMember(d => d.Email, o => o.MapFrom(s => s.User.Email))
            .ForMember(d => d.DepartmentName, o => o.MapFrom(s => s.Department != null ? s.Department.Name : null))
            .ForMember(d => d.Specializations,
                o => o.MapFrom(s => s.DoctorSpecializations.Select(ds => ds.Specialization.Name).ToList()))
            .ForMember(d => d.TotalAppointments, o => o.MapFrom(s => s.Appointments.Count));

        CreateMap<CreateDoctorDto, Doctor>();
        CreateMap<UpdateDoctorDto, Doctor>();

        // Appointment
        CreateMap<Appointment, AppointmentDto>()
            .ForMember(d => d.PatientName, o => o.MapFrom(s => s.Patient.User.FullName))
            .ForMember(d => d.DoctorName, o => o.MapFrom(s => s.Doctor.User.FullName));

        CreateMap<CreateAppointmentDto, Appointment>();
        CreateMap<UpdateAppointmentDto, Appointment>()
            .ForAllMembers(o => o.Condition((src, dest, srcMember) => srcMember != null));

        // Bill
        CreateMap<Bill, BillDto>()
            .ForMember(d => d.PatientName, o => o.MapFrom(s => s.Appointment.Patient.User.FullName))
            .ForMember(d => d.DoctorName, o => o.MapFrom(s => s.Appointment.Doctor.User.FullName));

        CreateMap<CreateBillDto, Bill>();
        CreateMap<UpdateBillDto, Bill>()
            .ForAllMembers(o => o.Condition((src, dest, srcMember) => srcMember != null));

        // Specialization
        CreateMap<Specialization, SpecializationDto>()
            .ForMember(d => d.DoctorCount, o => o.MapFrom(s => s.DoctorSpecializations.Count));

        CreateMap<CreateSpecializationDto, Specialization>();

        // Prescription
        CreateMap<Prescription, PrescriptionDto>()
            .ForMember(d => d.DoctorName, o => o.MapFrom(s => s.Doctor.User.FullName))
            .ForMember(d => d.PatientName, o => o.MapFrom(s => s.Appointment.Patient.User.FullName));

        CreateMap<PrescriptionMedicine, PrescriptionMedicineDto>()
            .ForMember(d => d.MedicineName, o => o.MapFrom(s => s.Medicine.Name));
    }
}
