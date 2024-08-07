using Domain.Interfaces.StoredProcedureBase;
using Domain.StoredProcedure;
using Infrastructure.Context;
using Infrastructure.StoredProcedures;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Repository.StoredProcedure
{
    public class EmployeeStoredProcedureRepository(AppDbContext dbContext)
        : RepositoryBaseStoredProcedure<EmployeeSpResult>(dbContext), IEmployeeStoredProcedureRepository
    {
        public async Task<EmployeeSpResult?> GetAsync(int id)
        {
            var parameter = new SqlParameter("@Id", id);
            return (await ExecuteAsync(EmployeeStoredProcedures.GetEmployeeById, parameter)).FirstOrDefault();
        }

        public async Task<IEnumerable<EmployeeSpResult>> ListAsync()
        {
            return await ExecuteAsync(EmployeeStoredProcedures.GetEmployees);
        }

        public async Task<StoredProcedureResult> UpdateAsync(EmployeeSpResult employee)
        {
            object[] parameters =
            [
                new SqlParameter("@Id", employee.Id),
                new SqlParameter("@Name", employee.Name),
                new SqlParameter("@Surname", employee.Surname),
                new SqlParameter("@Age", employee.Age),
                new SqlParameter("@Gender", (int)employee.Gender),
                new SqlParameter("@DepartmentId", (object?)employee.DepartmentId ?? DBNull.Value),
                new SqlParameter("@ProgrammingLanguageId", (object?)employee.ProgrammingLanguageId ?? DBNull.Value)
            ];

            return await ExecuteOperationAsync(EmployeeStoredProcedures.UpdateEmployee, parameters);
        }

        public async Task<StoredProcedureResult> CreateAsync(EmployeeSpResult employee)
        {
     
            object[] parameters =
            [
                new SqlParameter("@Name", employee.Name),
                new SqlParameter("@Surname", employee.Surname),
                new SqlParameter("@Age", employee.Age),
                new SqlParameter("@Gender", (int)employee.Gender),
                new SqlParameter("@DepartmentId", (object)employee.DepartmentId! ?? DBNull.Value),
                new SqlParameter("@ProgrammingLanguageId", (object)employee.ProgrammingLanguageId! ?? DBNull.Value)
              
            ];

            return await ExecuteOperationAsync(EmployeeStoredProcedures.CreateEmployee, parameters);
        }

        public async Task<StoredProcedureResult> DeleteAsync(int id)
        {
            var parameter = new SqlParameter("@Id", id);
            return await ExecuteOperationAsync(EmployeeStoredProcedures.DeleteEmployee, parameter);
        }
    }
}