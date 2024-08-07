using Application.Interfaces;
using Application.MappingProfiles;
using Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static void ConfigureApplication(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IEmployeeService, EmployeeService>();
        serviceCollection.AddScoped<IDepartmentService, DepartmentService>();
        serviceCollection.AddScoped<IProgrammingLanguageService, ProgrammingLanguageService>();
        
        serviceCollection.AddAutoMapper(typeof(EmployeeProfile));
        serviceCollection.AddAutoMapper(typeof(EmployeeProfile));
        serviceCollection.AddAutoMapper(typeof(EmployeeProfile));
    }
}