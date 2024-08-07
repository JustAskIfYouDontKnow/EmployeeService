using Domain.Interfaces.StoredProcedureBase;

namespace Domain.StoredProcedure;

public class StoredProcedureResult : IStoredProcedureResult
{
    public int Id { get; set; }
    public string? ErrorMessage { get; set; }
    public bool IsSuccess { get; set; }
 
}