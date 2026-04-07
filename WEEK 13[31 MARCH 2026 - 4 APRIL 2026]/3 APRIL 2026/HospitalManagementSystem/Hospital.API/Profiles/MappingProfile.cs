using AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDTO>();
        CreateMap<Department, DepartmentDTO>().ReverseMap();
         CreateMap<Doctor, DoctorDTO>().ReverseMap();
        CreateMap<RegisterDTO, User>()
            .ForMember(dest => dest.PasswordHash,
                       opt => opt.MapFrom(src => src.Password));
    }
}