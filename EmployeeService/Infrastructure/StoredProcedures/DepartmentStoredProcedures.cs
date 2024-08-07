namespace Infrastructure.StoredProcedures
{
    public static class DepartmentStoredProcedures
    {
        public const string GetDepartmentById = "GetDepartmentById @Id";
        public const string GetDepartments = "GetDepartments";
        public const string UpdateDepartment = "UpdateDepartment @Id, @Name, @Floor";
        public const string CreateDepartment = "CreateDepartment @Name, @Floor";
        public const string DeleteDepartment = "DeleteDepartment @Id";
    }
}