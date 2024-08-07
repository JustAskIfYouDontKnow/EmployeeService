using Domain.Interfaces;

namespace Domain.Entity;

public class ProgrammingLanguage : IEntity
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public ICollection<Employee> Employees { get; set; }
}