using Domain.Enums;
using Domain.StoredProcedure;
using Infrastructure.Context;
using Infrastructure.Repository.StoredProcedure;
using UnitTest.Fixture;
using Xunit;

namespace UnitTest
{
    [Collection("Database collection")]
    public class EmployeeStoredProcedureRepositoryTests(AppDbContextFixture fixture)
    {
        private readonly AppDbContext _context = fixture.DbContext;

        [Fact]
        public async Task GetAsync_ShouldReturnEmployee_WhenEmployeeExists()
        {
            // Arrange
            var repository = new EmployeeStoredProcedureRepository(_context);

            // Act
            var result = await repository.GetAsync(3);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Jake", result.Name);
        }

        [Fact]
        public async Task GetAsync_ShouldReturnNull_WhenEmployeeDoesNotExist()
        {
            // Arrange
            var repository = new EmployeeStoredProcedureRepository(_context);

            // Act
            var result = await repository.GetAsync(999);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateAsync_ShouldAddEmployee()
        {
            // Arrange
            var repository = new EmployeeStoredProcedureRepository(_context);
            var newEmployee = new EmployeeSpResult
            {
                Name = "Alice",
                Surname = "Smith",
                Age = 28,
                Gender = EmployeeGenderType.Female
            };

            // Act
            var result = await repository.CreateAsync(newEmployee);

            // Assert
            // Assert.True(result.IsSuccess == 1);
            var createdEmployee = await repository.GetAsync(result.Id);
            Assert.NotNull(createdEmployee);
            Assert.Equal("Alice", createdEmployee.Name);
            Assert.Equal("Smith", createdEmployee.Surname);
            Assert.Equal(28, createdEmployee.Age);
            Assert.Equal(EmployeeGenderType.Female, createdEmployee.Gender);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateEmployee()
        {
            // Arrange
            var repository = new EmployeeStoredProcedureRepository(_context);
            var employeeToUpdate = _context.Employees.First();
            var updatedEmployee = new EmployeeSpResult
            {
                Id = employeeToUpdate.Id,
                Name = "UpdatedName",
                Surname = "UpdatedSurname",
                Age = 35,
                Gender = EmployeeGenderType.Female,
                DepartmentId = 1,
                ProgrammingLanguageId = 1
            };

            // Act
            var result = await repository.UpdateAsync(updatedEmployee);

            // Assert
            // Assert.True(result.IsSuccess == 1);

            var updatedEmployeeFromDb = await repository.GetAsync(employeeToUpdate.Id);

            Assert.NotNull(updatedEmployeeFromDb);
            Assert.Equal("UpdatedName", updatedEmployeeFromDb.Name);
            Assert.Equal("UpdatedSurname", updatedEmployeeFromDb.Surname);
            Assert.Equal(35, updatedEmployeeFromDb.Age);
            Assert.Equal(1, updatedEmployeeFromDb.DepartmentId);
            Assert.Equal(1, updatedEmployeeFromDb.ProgrammingLanguageId);
        }


        [Fact]
        public async Task UpdateAsync_ErrorUpdateEmployee()
        {
            // Arrange
            var repository = new EmployeeStoredProcedureRepository(_context);
            var employeeToUpdate = await repository.GetAsync(4);
            Assert.NotNull(employeeToUpdate);
            
            var updatedEmployee = new EmployeeSpResult
            {
                Id = employeeToUpdate.Id,
                Name = "UpdatedName",
                Surname = "UpdatedSurname",
                Age = 35,
                Gender = EmployeeGenderType.Female,
                DepartmentId = 999,
                ProgrammingLanguageId = 999
            };

            // Act
            var result = await repository.UpdateAsync(updatedEmployee);

            // Assert
            Assert.False(result.IsSuccess);

            var updatedEmployeeFromDb = await repository.GetAsync(employeeToUpdate.Id);
            
            Assert.NotNull(updatedEmployeeFromDb);
            Assert.Equal(employeeToUpdate.Name, updatedEmployeeFromDb.Name);
            Assert.Equal(employeeToUpdate.Surname, updatedEmployeeFromDb.Surname);
            Assert.Equal(employeeToUpdate.DepartmentId, updatedEmployeeFromDb.DepartmentId);
            Assert.Equal(employeeToUpdate.ProgrammingLanguageId, updatedEmployeeFromDb.ProgrammingLanguageId);
        }


        [Fact]
        public async Task DeleteAsync_ShouldRemoveEmployee()
        {
            // Arrange
            var repository = new EmployeeStoredProcedureRepository(_context);
            var employeeToDelete = _context.Employees.First();

            // Act
            StoredProcedureResult result = await repository.DeleteAsync(employeeToDelete.Id);

            // Assert
            Assert.True(result.IsSuccess);
            var deletedEmployee = await repository.GetAsync(employeeToDelete.Id);
            Assert.Null(deletedEmployee);
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrowException_WhenEmployeeNotExists()
        {
            // Arrange
            var repository = new EmployeeStoredProcedureRepository(_context);
            var nonExistingEmployee = new EmployeeSpResult
            {
                Id = 999,
                Name = "NonExisting",
                Surname = "Employee",
                Age = 40,
                Gender = EmployeeGenderType.Male
            };

            // Act & Assert
            var result = await repository.UpdateAsync(nonExistingEmployee);
            Assert.Equal(0, result.Id);
            Assert.NotNull(result.ErrorMessage);
            Assert.False(result.IsSuccess);
        }
        
        
        [Fact]
        public async Task GetAllAsync_ShouldReturnValue()
        {
            // Arrange
            var repository = new EmployeeStoredProcedureRepository(_context);
            var result = await repository.ListAsync();

            // Assert
            Assert.NotNull(result);
        }
    }
}