namespace Infrastructure.StoredProcedures
{
    public static class ProgrammingLanguageStoredProcedures
    {
        public const string GetProgrammingLanguageById = "GetProgrammingLanguageById @Id";
        public const string GetProgrammingLanguages = "GetProgrammingLanguages";
        public const string UpdateProgrammingLanguage = "UpdateProgrammingLanguage @Id, @Name";
        public const string CreateProgrammingLanguage = "CreateProgrammingLanguage @Name";
        public const string DeleteProgrammingLanguage = "DeleteProgrammingLanguage @Id";
    }
}