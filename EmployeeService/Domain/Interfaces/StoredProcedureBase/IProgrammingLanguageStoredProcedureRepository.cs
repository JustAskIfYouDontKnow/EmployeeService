using Domain.StoredProcedure;

namespace Domain.Interfaces.StoredProcedureBase;

public interface IProgrammingLanguageStoredProcedureRepository : IStoredProcedureRepository<ProgrammingLanguageSpResult>
{
    Task<IEnumerable<ProgrammingLanguageSpResult>> ListAsync();
    Task<ProgrammingLanguageSpResult?> GetAsync(int id);
    Task<StoredProcedureResult> UpdateAsync(ProgrammingLanguageSpResult employee);
    Task<StoredProcedureResult> CreateAsync(ProgrammingLanguageSpResult employee);
    Task<StoredProcedureResult> DeleteAsync(int id);
}