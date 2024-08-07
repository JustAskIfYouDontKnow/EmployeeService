using Domain.Interfaces.StoredProcedureBase;
using Domain.StoredProcedure;
using Infrastructure.Context;
using Infrastructure.StoredProcedures;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Repository.StoredProcedure
{
    public class DepartmentStoredProcedureRepository(AppDbContext dbContext)
        : RepositoryBaseStoredProcedure<DepartmentSpResult>(dbContext), IDepartmentStoredProcedureRepository
    {
        public async Task<DepartmentSpResult?> GetAsync(int id)
        {
            var parameter = new SqlParameter("@Id", id);
            return (await ExecuteAsync(DepartmentStoredProcedures.GetDepartmentById, parameter)).FirstOrDefault();
        }

        public async Task<IEnumerable<DepartmentSpResult>> ListAsync()
        {
            return await ExecuteAsync(DepartmentStoredProcedures.GetDepartments);
        }

        public async Task<StoredProcedureResult> UpdateAsync(DepartmentSpResult department)
        {
            object[] parameters =
            [
                new SqlParameter("@Id", department.Id),
                new SqlParameter("@Name", department.Name),
                new SqlParameter("@Floor", department.Floor)
            ];
            return await ExecuteOperationAsync(DepartmentStoredProcedures.UpdateDepartment, parameters);
        }

        public async Task<StoredProcedureResult> CreateAsync(DepartmentSpResult department)
        {
            object[] parameters =
            [
                new SqlParameter("@Name", department.Name),
                new SqlParameter("@Floor", department.Floor)
            ];

            return await ExecuteOperationAsync(DepartmentStoredProcedures.CreateDepartment, parameters);
        }

        public async Task<StoredProcedureResult> DeleteAsync(int id)
        {
            var parameter = new SqlParameter("@Id", id);
           return await ExecuteOperationAsync(DepartmentStoredProcedures.DeleteDepartment, parameter);
           
        }
    }
}
