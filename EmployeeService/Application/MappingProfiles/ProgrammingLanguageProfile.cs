using Application.DTO;
using AutoMapper;
using Domain.Entity;
using Domain.StoredProcedure;

namespace Application.MappingProfiles;

public class ProgrammingLanguageProfile : Profile
{
    public ProgrammingLanguageProfile()
    {
        CreateMap<ProgrammingLanguage, ProgrammingLanguageDto>().ReverseMap();
        CreateMap<ProgrammingLanguageSpResult, ProgrammingLanguageDto>().ReverseMap();
    }
}