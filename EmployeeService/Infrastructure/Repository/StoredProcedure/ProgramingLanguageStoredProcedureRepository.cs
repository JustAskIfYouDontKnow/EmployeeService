using Domain.Interfaces.StoredProcedureBase;
using Domain.StoredProcedure;
using Infrastructure.Context;
using Infrastructure.StoredProcedures;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Repository.StoredProcedure
{
    public class ProgrammingLanguageStoredProcedureRepository(AppDbContext dbContext)
        : RepositoryBaseStoredProcedure<ProgrammingLanguageSpResult>(dbContext),
            IProgrammingLanguageStoredProcedureRepository
    {
        public async Task<ProgrammingLanguageSpResult?> GetAsync(int id)
        {
            var parameter = new SqlParameter("@Id", id);
            return (await ExecuteAsync(ProgrammingLanguageStoredProcedures.GetProgrammingLanguageById, parameter)).FirstOrDefault();
        }

        public async Task<IEnumerable<ProgrammingLanguageSpResult>> ListAsync()
        {
            return await ExecuteAsync(ProgrammingLanguageStoredProcedures.GetProgrammingLanguages);
        }

        public async Task<StoredProcedureResult> UpdateAsync(ProgrammingLanguageSpResult programmingLanguage)
        {
            object[] parameters =
            [
                new SqlParameter("@Id", programmingLanguage.Id),
                new SqlParameter("@Name", programmingLanguage.Name)
            ];

            return await ExecuteOperationAsync(ProgrammingLanguageStoredProcedures.UpdateProgrammingLanguage, parameters);
        }

        public async Task<StoredProcedureResult> CreateAsync(ProgrammingLanguageSpResult programmingLanguage)
        {
            object[] parameters =
            [
                new SqlParameter("@Name", programmingLanguage.Name)
            ];

            return await ExecuteOperationAsync(ProgrammingLanguageStoredProcedures.CreateProgrammingLanguage, parameters);
        }

        public async Task<StoredProcedureResult> DeleteAsync(int id)
        {
            var parameter = new SqlParameter("@Id", id);
            return await ExecuteOperationAsync(ProgrammingLanguageStoredProcedures.DeleteProgrammingLanguage, parameter);
        }
    }
}
