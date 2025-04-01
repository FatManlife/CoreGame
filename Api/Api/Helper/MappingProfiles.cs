using Api.Models.DTOs;
using Api.Models;
using AutoMapper;

namespace Api.Helper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<User, UserDto>();
        CreateMap<UserDto, User>();
        CreateMap<User, LoginDto>();
        CreateMap<RegisterDto, User>();
        CreateMap<User, RegisterDto>();
        CreateMap<PublisherDto, Publisher>();
        CreateMap<AddGameDto, Game>();
        CreateMap<Game,ShowGameDto>()
            .ForMember(dest => dest.GenreNames, opt => opt.MapFrom(src => src.Genres.Select(g => g.Name)))
            .ForMember(dest => dest.ModeNames, opt => opt.MapFrom(src => src.Modes.Select(m => m.Name)))
            .ForMember(dest => dest.PlatformNames, opt => opt.MapFrom(src => src.Platforms.Select(p => p.Name)))
            .ForMember(dest => dest.Developer, opt => opt.MapFrom(src => src.Developer.Username))  
            .ForMember(dest => dest.PublisherName, opt => opt.MapFrom(src => src.Publisher.Name));
        CreateMap<SpecDto,Spec>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        CreateMap<ImageGameDto, Image>();
        CreateMap<Image,ImageGameDto>();
        CreateMap<UpdateGameDto, Game>();

    }
}