using Api.Models.DTOs;
using Api.Models;
using AutoMapper;

namespace Api.Helper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Image, ImageDto>();
        CreateMap<User, UserDto>();
        CreateMap<UserDto, User>();
        CreateMap<User, LoginDto>();
        CreateMap<RegisterDto, User>();
        CreateMap<User, RegisterDto>();
    }
}