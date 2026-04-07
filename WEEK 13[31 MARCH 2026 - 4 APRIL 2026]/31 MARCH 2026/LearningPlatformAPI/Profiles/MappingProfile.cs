using AutoMapper;
using LearningPlatformAPI.Models;
using LearningPlatformAPI.DTOs;

namespace LearningPlatformAPI.Profiles;

public class MappingProfile : AutoMapper.Profile
{
    public MappingProfile()
    {
        CreateMap<Course, CourseDto>().ReverseMap();
        CreateMap<Lesson, LessonDto>().ReverseMap();
        CreateMap<User, UserDto>().ReverseMap();
    }
}