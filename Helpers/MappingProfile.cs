
using GeneratorUsingStoreProcedures.DTOs;
using GeneratorUsingStoreProcedures.Entities;
using AutoMapper;

namespace GeneratorUsingStoreProcedures.Helpers;


public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<PersonalDto, Personal>()
            .ForMember(dest => dest.GenderId, opt => opt.Ignore())
            .ForMember(dest => dest.HobbyId, opt => opt.Ignore());
        CreateMap<Personal, PersonalDto>()
            .ForMember(dest => dest.GenderName, opt => opt.MapFrom(src => src.Gender.Name))
            .ForMember(dest => dest.HobbyName, opt => opt.MapFrom(src => src.Hobby.Name));
    }
}
