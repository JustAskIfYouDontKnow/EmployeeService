using Application.DTO;
using AutoMapper;
using Domain.Entity;
using Domain.StoredProcedure;

namespace Application.MappingProfiles;

public class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        CreateMap<Employee, EmployeeDto>()
            .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department))
            .ForMember(dest => dest.ProgrammingLanguage, opt => opt.MapFrom(src => src.ProgrammingLanguage))
            .ReverseMap();

        CreateMap<EmployeeSpResult, EmployeeDto>()
            .ForMember(dest => dest.Department, opt => opt.MapFrom(src => new DepartmentDto
            {
                Id = src.DepartmentId,
                Name = src.DepartmentName,
                Floor = src.DepartmentFloor
            }))
            .ForMember(dest => dest.ProgrammingLanguage, opt => opt.MapFrom(src => new ProgrammingLanguageDto
            {
                Id = src.ProgrammingLanguageId,
                Name = src.ProgrammingLanguageName
            })).ReverseMap();
        
    }
}