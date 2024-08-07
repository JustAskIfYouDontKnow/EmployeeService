using Domain.StoredProcedure;

namespace Domain.Interfaces.StoredProcedureBase;

public interface IEmployeeStoredProcedureRepository : IStoredProcedureRepository<EmployeeSpResult>
{
    Task<IEnumerable<EmployeeSpResult>> ListAsync();
    Task<EmployeeSpResult?> GetAsync(int id);
    Task<StoredProcedureResult> UpdateAsync(EmployeeSpResult employee);
    Task<StoredProcedureResult> CreateAsync(EmployeeSpResult employee);
    Task<StoredProcedureResult> DeleteAsync(int id);
}