using Domain.Entity;
using Domain.Enums;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace UnitTest.Fixture
{
    public class AppDbContextFixture : IDisposable
    {
        public AppDbContext DbContext { get; private set; }

        public AppDbContextFixture()
        {
            var databaseName = $"TestDb_{Guid.NewGuid()}";
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer(CreateDatabaseConnectionString(databaseName))
                .Options;

            DbContext = new AppDbContext(options);
            ApplyMigrationsAsync().GetAwaiter().GetResult();
            SeedDatabaseAsync().GetAwaiter().GetResult();
        }

        private static string CreateDatabaseConnectionString(string databaseName)
        {
            return $"Server=localhost;Database={databaseName};User=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=True;";
        }

        private async Task ApplyMigrationsAsync()
        {
            await DbContext.Database.MigrateAsync();
        }

        private async Task SeedDatabaseAsync()
        {
            await DbContext.Employees.AddRangeAsync(
                new Employee { Name = "John", Surname = "Doe", Age = 30, Gender = EmployeeGenderType.Male },
                new Employee { Name = "Jane", Surname = "Doe", Age = 25, Gender = EmployeeGenderType.Female },
                new Employee { Name = "Jake", Surname = "Doe", Age = 24, Gender = EmployeeGenderType.Male },
                new Employee { Name = "July", Surname = "Doe", Age = 34, Gender = EmployeeGenderType.Female }
            );

            await DbContext.Departments.AddRangeAsync(
                new Department { Name = "IT", Floor = 1 },
                new Department { Name = "HR", Floor = 2 },
                new Department { Name = "Sales", Floor = 2 }
            );

            await DbContext.ProgrammingLanguages.AddRangeAsync(
                new ProgrammingLanguage { Name = "C#" },
                new ProgrammingLanguage { Name = "Java" }
            );

            await DbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            DbContext.Database.EnsureDeleted();
            DbContext.Dispose();
        }
    }

    [CollectionDefinition("Database collection")]
    public class DatabaseCollection : ICollectionFixture<AppDbContextFixture> { }
}