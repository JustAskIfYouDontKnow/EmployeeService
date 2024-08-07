
using Domain.StoredProcedure;

namespace Domain.Interfaces.StoredProcedureBase;

public interface IDepartmentStoredProcedureRepository : IStoredProcedureRepository<DepartmentSpResult>
{
    Task<IEnumerable<DepartmentSpResult>> ListAsync();
    Task<DepartmentSpResult?> GetAsync(int id);
    Task<StoredProcedureResult> UpdateAsync(DepartmentSpResult employee);
    Task<StoredProcedureResult> CreateAsync(DepartmentSpResult employee);
    Task<StoredProcedureResult> DeleteAsync(int id);
}