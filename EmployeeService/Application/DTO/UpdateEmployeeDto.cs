using Domain.Enums;

namespace Application.DTO;

public class UpdateEmployeeDto : AbstractDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public int Age { get; set; }
    public EmployeeGenderType Gender { get; set; }
    public int DepartmentId { get; set; }
    public int ProgrammingLanguageId { get; set; }
}