using System.Data;
using Domain.Interfaces.StoredProcedureBase;
using Domain.StoredProcedure;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.StoredProcedure;

public class RepositoryBaseStoredProcedure<T> : IStoredProcedureRepository<T> where T : class, IStoredProcedureResult
{
    private readonly DbContext _dbContext; 
    private readonly DbSet<T> _dbSet;

    protected RepositoryBaseStoredProcedure(DbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>();
    }

    public virtual async Task<IEnumerable<T>> ExecuteAsync(string storedProcedure, params object[] parameters)
    {
        return await _dbSet.FromSqlRaw("EXEC " + storedProcedure, parameters).ToListAsync();
    }
    
    public virtual async Task<StoredProcedureResult> ExecuteOperationAsync(string storedProcedure, params object[] parameters)
    {
        await using (var command = _dbContext.Database.GetDbConnection().CreateCommand())
        {
            command.CommandText = "EXEC " + storedProcedure;
            command.CommandType = CommandType.Text;

            foreach (var parameter in parameters)
            {
                command.Parameters.Add(parameter);
            }

            await _dbContext.Database.OpenConnectionAsync();
            var result = new StoredProcedureResult();

            try
            {
                await using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        result.ErrorMessage = reader["ErrorMessage"] != DBNull.Value ? reader["ErrorMessage"].ToString() : null;
                        result.IsSuccess = reader["IsSuccess"] != DBNull.Value && Convert.ToBoolean(reader["IsSuccess"]);
                        result.Id = reader["Id"] != DBNull.Value ? Convert.ToInt32(reader["Id"]) : 0;
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
                result.IsSuccess = false;
            }
            finally
            {
                await _dbContext.Database.CloseConnectionAsync();
            }

            return result;
        }
    }
}
