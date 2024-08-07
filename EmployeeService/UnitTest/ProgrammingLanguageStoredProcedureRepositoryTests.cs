using Domain.StoredProcedure;
using Infrastructure.Context;
using Infrastructure.Repository.StoredProcedure;
using UnitTest.Fixture;
using Xunit;

namespace UnitTest;

[Collection("Database collection")]
public class ProgrammingLanguageStoredProcedureRepositoryTests(AppDbContextFixture fixture)
{
    private readonly AppDbContext _context = fixture.DbContext;

    [Fact]
    public async Task GetAsync_ShouldReturnProgrammingLanguage_WhenProgrammingLanguageExists()
    {
        // Arrange
        var repository = new ProgrammingLanguageStoredProcedureRepository(_context);

        // Act
        var result = await repository.GetAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("C#", result.Name);
    }

    [Fact]
    public async Task GetAsync_ShouldReturnNull_WhenProgrammingLanguageDoesNotExist()
    {
        // Arrange
        var repository = new ProgrammingLanguageStoredProcedureRepository(_context);

        // Act
        var result = await repository.GetAsync(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task CreateAsync_ShouldAddProgrammingLanguage()
    {
        // Arrange
        var repository = new ProgrammingLanguageStoredProcedureRepository(_context);
        var newProgrammingLanguage = new ProgrammingLanguageSpResult
        {
            Name = "Python"
        };

        // Act
        var result = await repository.CreateAsync(newProgrammingLanguage);

        // Assert
        Assert.True(result.IsSuccess);
        var createdProgrammingLanguage = await repository.GetAsync(result.Id);
        Assert.NotNull(createdProgrammingLanguage);
        Assert.Equal("Python", createdProgrammingLanguage.Name);
    }

    [Fact]
    public async Task UpdateAsync_ShouldUpdateProgrammingLanguage()
    {
        // Arrange
        var repository = new ProgrammingLanguageStoredProcedureRepository(_context);
        var programmingLanguageToUpdate = _context.ProgrammingLanguages.First();
        var updatedProgrammingLanguage = new ProgrammingLanguageSpResult
        {
            Id = programmingLanguageToUpdate.Id,
            Name = "UpdatedName"
        };

        // Act
        var result = await repository.UpdateAsync(updatedProgrammingLanguage);

        // Assert
        Assert.True(result.IsSuccess);

        var updatedProgrammingLanguageFromDb = await repository.GetAsync(programmingLanguageToUpdate.Id);

        Assert.NotNull(updatedProgrammingLanguageFromDb);
        Assert.Equal("UpdatedName", updatedProgrammingLanguageFromDb.Name);
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveProgrammingLanguage()
    {
        // Arrange
        var repository = new ProgrammingLanguageStoredProcedureRepository(_context);
        var programmingLanguageToDelete = _context.ProgrammingLanguages.First();

        // Act
        var result = await repository.DeleteAsync(programmingLanguageToDelete.Id);

        // Assert
        Assert.True(result.IsSuccess);
        var deletedProgrammingLanguage = await repository.GetAsync(programmingLanguageToDelete.Id);
        Assert.Null(deletedProgrammingLanguage);
    }
}