namespace EcommerceAPI;
using AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDTO>();
        CreateMap<RegisterDTO, User>();
    }
}