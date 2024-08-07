using Domain.StoredProcedure;
using Infrastructure.Context;
using Infrastructure.Repository.StoredProcedure;
using UnitTest.Fixture;
using Xunit;

namespace UnitTest;

[Collection("Database collection")]
public class DepartmentStoredProcedureRepositoryTests(AppDbContextFixture fixture)
{
    private readonly AppDbContext _context = fixture.DbContext;

    [Fact]
    public async Task GetAsync_ShouldReturnDepartment_WhenDepartmentExists()
    {
        // Arrange
        var repository = new DepartmentStoredProcedureRepository(_context);

        // Act
        var result = await repository.GetAsync(3);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Sales", result.Name);
    }

    [Fact]
    public async Task GetAsync_ShouldReturnNull_WhenDepartmentDoesNotExist()
    {
        // Arrange
        var repository = new DepartmentStoredProcedureRepository(_context);

        // Act
        var result = await repository.GetAsync(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task CreateAsync_ShouldAddDepartment()
    {
        // Arrange
        var repository = new DepartmentStoredProcedureRepository(_context);
        var newDepartment = new DepartmentSpResult
        {
            Name = "HR",
            Floor = 2
        };

        // Act
        StoredProcedureResult result = await repository.CreateAsync(newDepartment);

        // Assert
        Assert.True(result.IsSuccess);
        var createdDepartment = await repository.GetAsync(result.Id);
        Assert.NotNull(createdDepartment);
        Assert.Equal("HR", createdDepartment.Name);
        Assert.Equal(2, createdDepartment.Floor);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateDepartment()
    {
        // Arrange
        var repository = new DepartmentStoredProcedureRepository(_context);
        var departmentToUpdate = _context.Departments.First();
        var updatedDepartment = new DepartmentSpResult
        {
            Id = departmentToUpdate.Id,
            Name = "UpdatedName",
            Floor = 3
        };

        // Act
        var result = await repository.UpdateAsync(updatedDepartment);

        // Assert
        Assert.True(result.IsSuccess);

        var updatedDepartmentFromDb = await repository.GetAsync(departmentToUpdate.Id);

        Assert.NotNull(updatedDepartmentFromDb);
        Assert.Equal("UpdatedName", updatedDepartmentFromDb.Name);
        Assert.Equal(3, updatedDepartmentFromDb.Floor);
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveDepartment()
    {
        // Arrange
        var repository = new DepartmentStoredProcedureRepository(_context);
        var departmentToDelete = _context.Departments.First();

        // Act
        var result = await repository.DeleteAsync(departmentToDelete.Id);

        // Assert
        Assert.True(result.IsSuccess);
        var deletedDepartment = await repository.GetAsync(departmentToDelete.Id);
        Assert.Null(deletedDepartment);
    }
    
    [Fact]
    public async Task GetAllAsync_ShouldReturnValue()
    {
        // Arrange
        var repository = new DepartmentStoredProcedureRepository(_context);
        var result = await repository.ListAsync();

        // Assert
        Assert.NotNull(result);
    }
}