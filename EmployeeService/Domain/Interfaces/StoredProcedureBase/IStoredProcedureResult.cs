namespace Domain.Interfaces.StoredProcedureBase;

public interface IStoredProcedureResult
{
    public int Id { get; set; }
    public string? ErrorMessage { get; set; }
    public bool IsSuccess { get; set; }
}
