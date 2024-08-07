using Domain.Enums;

namespace Domain.StoredProcedure;

public class EmployeeSpResult : StoredProcedureResult
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public int Age { get; set; }
    public EmployeeGenderType Gender { get; set; }
    public int? DepartmentId { get; set; }
    public string? DepartmentName { get; set; }
    public int? DepartmentFloor { get; set; }
    public int? ProgrammingLanguageId { get; set; }
    public string? ProgrammingLanguageName { get; set; }
}