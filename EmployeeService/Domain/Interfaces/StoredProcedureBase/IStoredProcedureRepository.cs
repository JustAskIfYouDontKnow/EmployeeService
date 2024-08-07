namespace Domain.Interfaces.StoredProcedureBase;

public interface IStoredProcedureRepository<T> where T : IStoredProcedureResult
{
    Task<IEnumerable<T>> ExecuteAsync(string storedProcedure, params object[] parameters);
    Task<StoredProcedure.StoredProcedureResult> ExecuteOperationAsync(string storedProcedure, params object[] parameters);
}