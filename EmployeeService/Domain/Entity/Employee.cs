using Domain.Enums;
using Domain.Interfaces;

namespace Domain.Entity;

public class Employee : IEntity
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public int Age { get; set; }
    public EmployeeGenderType Gender { get; set; }
    public int? DepartmentId { get; set; }
    public Department? Department { get; set; }
    public int? ProgrammingLanguageId { get; set; }
    public ProgrammingLanguage? ProgrammingLanguage { get; set; }
}
