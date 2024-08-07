using Domain.Entity;
using Domain.Enums;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DataSeed
{
    public static void ExecuteSeedData(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        context.Database.Migrate();

        if (context.Employees.Any()) return;
        
        var departmentIt = new Department { Name = "IT", Floor = 1 };
        var departmentHr = new Department { Name = "HR", Floor = 2 };
        var departmentSales = new Department { Name = "Sales", Floor = 2 };
            
        var languageCsharp = new ProgrammingLanguage { Name = "C#" };
        var languageJava = new ProgrammingLanguage { Name = "Java" };
        var typescript = new ProgrammingLanguage { Name = "TypeScript" };

        context.Departments.AddRange(departmentIt, departmentHr, departmentSales);
        context.ProgrammingLanguages.AddRange(languageCsharp, languageJava, typescript);

        context.Employees.AddRange(
            new Employee
            {
                Name = "John",
                Surname = "Doe",
                Age = 30,
                Gender = EmployeeGenderType.Male,
                Department = departmentIt,
                ProgrammingLanguage = languageCsharp
            },
            new Employee
            {
                Name = "Jane",
                Surname = "Doe",
                Age = 22,
                Gender = EmployeeGenderType.Female,
                Department = departmentHr,
                ProgrammingLanguage = languageJava
            });

        context.SaveChanges();
    }
}