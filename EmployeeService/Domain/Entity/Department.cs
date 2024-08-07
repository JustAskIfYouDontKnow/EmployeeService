using Domain.Interfaces;

namespace Domain.Entity;

public class Department : IEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Floor { get; set; }
    public ICollection<Employee> Employees { get; set; }
}