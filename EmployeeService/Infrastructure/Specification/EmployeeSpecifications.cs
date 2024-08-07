using Domain.Entity;

namespace Infrastructure.Specification;

public class EmployeeSpecifications :  BaseSpecification<Employee>
{
    public EmployeeSpecifications(int minAge) : base(customer => customer.Age >= minAge)
    {
        AddInclude(customer => customer.Department);
        AddInclude(customer => customer.ProgrammingLanguage);
    }
}