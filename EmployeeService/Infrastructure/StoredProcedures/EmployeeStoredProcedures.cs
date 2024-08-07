namespace Infrastructure.StoredProcedures
{
    public static class EmployeeStoredProcedures
    {
        public const string GetEmployeeById = "GetEmployeeById @Id";
        public const string GetEmployees    = "GetEmployees";
        public const string UpdateEmployee  = "UpdateEmployee @Id, @Name, @Surname, @Age, @Gender, @DepartmentId, @ProgrammingLanguageId";
        public const string CreateEmployee  = "CreateEmployee @Name, @Surname, @Age, @Gender, @DepartmentId, @ProgrammingLanguageId";
        public const string DeleteEmployee  = "DeleteEmployee @Id";
    }
}