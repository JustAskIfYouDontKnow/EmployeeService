using Domain.Interfaces;
using Domain.Interfaces.StoredProcedureBase;
using Infrastructure.Context;
using Infrastructure.Repository;
using Infrastructure.Repository.StoredProcedure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static void ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var isDockerEnv = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";

        var connectionString = isDockerEnv
            ? configuration.GetConnectionString("DockerDbConnection")
            : configuration.GetConnectionString("LocalDbConnection");
        
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IEmployeeStoredProcedureRepository, EmployeeStoredProcedureRepository>();
        services.AddScoped<IProgrammingLanguageStoredProcedureRepository, ProgrammingLanguageStoredProcedureRepository>();
        services.AddScoped<IDepartmentStoredProcedureRepository, DepartmentStoredProcedureRepository>();
    }
}