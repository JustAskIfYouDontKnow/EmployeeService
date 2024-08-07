using Domain.Enums;

namespace Application.DTO;

public class EmployeeDto : AbstractDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public int Age { get; set; }
    public EmployeeGenderType Gender { get; set; }
    public DepartmentDto? Department { get; set; }
    public ProgrammingLanguageDto? ProgrammingLanguage { get; set; }
}