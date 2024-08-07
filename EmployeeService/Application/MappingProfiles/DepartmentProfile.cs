using Application.DTO;
using AutoMapper;
using Domain.Entity;
using Domain.StoredProcedure;

namespace Application.MappingProfiles;

public class DepartmentProfile : Profile
{
    public DepartmentProfile()
    {
        CreateMap<Department, DepartmentDto>().ReverseMap();
        CreateMap<DepartmentSpResult, DepartmentDto>().ReverseMap();
    }
}