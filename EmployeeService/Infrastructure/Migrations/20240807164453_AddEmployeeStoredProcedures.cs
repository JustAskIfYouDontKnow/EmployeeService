using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddEmployeeStoredProcedures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROCEDURE UpdateEmployee @Id INT,
                                    @Name NVARCHAR(50),
                                    @Surname NVARCHAR(50),
                                    @Age INT,
                                    @Gender INT,
                                    @DepartmentId INT,
                                    @ProgrammingLanguageId INT
            AS
            BEGIN
                SET NOCOUNT ON;
                DECLARE @ErrorMessage NVARCHAR(4000);
                DECLARE @IsSuccess BIT = 1;

                BEGIN TRY
                    BEGIN TRANSACTION;

                    UPDATE Employees
                    SET Name                  = @Name,
                        Surname               = @Surname,
                        Age                   = @Age,
                        Gender                = @Gender,
                        DepartmentId          = @DepartmentId,
                        ProgrammingLanguageId = @ProgrammingLanguageId
                    WHERE Id = @Id;

                    IF @@ROWCOUNT = 0
                    BEGIN
                        RAISERROR ('Employee with Id %d not found.', 16, 1, @Id);
                    END
                    COMMIT TRANSACTION;

                    SET @IsSuccess = 1;
                    SELECT Id, '' AS ErrorMessage, CAST(@IsSuccess AS BIT) AS IsSuccess
                    FROM Employees
                    WHERE Id = @Id;

                END TRY
                BEGIN CATCH
                    IF @@TRANCOUNT > 0
                        ROLLBACK;

                    SET @IsSuccess = 0;
                    SELECT NULL AS Id, ERROR_MESSAGE() AS ErrorMessage, CAST(@IsSuccess AS BIT) AS IsSuccess;
                END CATCH
            END
            GO");

            migrationBuilder.Sql(@"CREATE PROCEDURE GetEmployees
            AS
            BEGIN
                SET NOCOUNT ON;
                DECLARE @ErrorMessage NVARCHAR(4000);
                DECLARE @IsSuccess BIT = 1;

                BEGIN TRY
                    BEGIN TRANSACTION;

                    SELECT e.Id,
                           e.Name,
                           e.Surname,
                           e.Age,
                           e.Gender,
                           d.Id    AS DepartmentId,
                           d.Name  AS DepartmentName,
                           d.Floor AS DepartmentFloor,
                           p.Id    AS ProgrammingLanguageId,
                           p.Name  AS ProgrammingLanguageName,
                           '' AS ErrorMessage,
                           CAST(@IsSuccess AS BIT) AS IsSuccess
                    FROM Employees e
                    LEFT JOIN Departments d ON e.DepartmentId = d.Id
                    LEFT JOIN ProgrammingLanguages p ON e.ProgrammingLanguageId = p.Id;

                    COMMIT TRANSACTION;
                END TRY
                BEGIN CATCH
                    IF @@TRANCOUNT > 0
                        ROLLBACK;

                    SET @IsSuccess = 0;
                    SELECT ERROR_MESSAGE() AS ErrorMessage, CAST(@IsSuccess AS BIT) AS IsSuccess;
                END CATCH
            END
            GO");

            migrationBuilder.Sql(@"CREATE PROCEDURE GetEmployeeById @Id INT
            AS
            BEGIN
                SET NOCOUNT ON;
                DECLARE @ErrorMessage NVARCHAR(4000);
                DECLARE @IsSuccess BIT = 1;

                BEGIN TRY
                    BEGIN TRANSACTION;

                    SELECT e.Id,
                           e.Name,
                           e.Surname,
                           e.Age,
                           e.Gender,
                           d.Id    AS DepartmentId,
                           d.Name  AS DepartmentName,
                           d.Floor AS DepartmentFloor,
                           p.Id    AS ProgrammingLanguageId,
                           p.Name  AS ProgrammingLanguageName,
                           '' AS ErrorMessage,
                           CAST(@IsSuccess AS BIT) AS IsSuccess
                    FROM Employees e
                    LEFT JOIN Departments d ON e.DepartmentId = d.Id
                    LEFT JOIN ProgrammingLanguages p ON e.ProgrammingLanguageId = p.Id
                    WHERE e.Id = @Id;

                    IF @@ROWCOUNT = 0
                    BEGIN
                        RAISERROR ('Employee with Id %d not found.', 16, 1, @Id);
                    END
                    COMMIT TRANSACTION;
                END TRY
                BEGIN CATCH
                    IF @@TRANCOUNT > 0
                        ROLLBACK;

                    SET @IsSuccess = 0;
                    SELECT NULL AS Id, ERROR_MESSAGE() AS ErrorMessage, CAST(@IsSuccess AS BIT) AS IsSuccess;
                END CATCH
            END
            GO");

            migrationBuilder.Sql(@"CREATE PROCEDURE DeleteEmployee @Id INT
            AS
            BEGIN
                SET NOCOUNT ON;
                DECLARE @ErrorMessage NVARCHAR(4000);
                DECLARE @IsSuccess BIT = 1;

                BEGIN TRY
                    BEGIN TRANSACTION;

                    DELETE FROM Employees WHERE Id = @Id;

                    IF @@ROWCOUNT = 0
                    BEGIN
                        RAISERROR ('Employee with Id %d not found.', 16, 1, @Id);
                    END

                    SET @IsSuccess = 1;
                    SELECT 0 As Id,
                           ''                      AS ErrorMessage,
                           CAST(@IsSuccess AS BIT) AS IsSuccess

                    COMMIT TRANSACTION;
                END TRY
                BEGIN CATCH
                    IF @@TRANCOUNT > 0
                        ROLLBACK;

                    SET @IsSuccess = 0;
                    SELECT ERROR_MESSAGE() AS ErrorMessage, CAST(@IsSuccess AS BIT) AS IsSuccess;
                END CATCH
            END
            GO");

            migrationBuilder.Sql(@"CREATE PROCEDURE CreateEmployee @Name NVARCHAR(50),
                                    @Surname NVARCHAR(50),
                                    @Age INT,
                                    @Gender INT,
                                    @DepartmentId INT,
                                    @ProgrammingLanguageId INT
            AS
            BEGIN
                SET NOCOUNT ON;
                DECLARE @ErrorMessage NVARCHAR(4000);
                DECLARE @IsSuccess BIT = 1;
                DECLARE @NewId INT;

                BEGIN TRY
                    BEGIN TRANSACTION;

                    INSERT INTO Employees (Name, Surname, Age, Gender, DepartmentId, ProgrammingLanguageId)
                    VALUES (@Name, @Surname, @Age, @Gender, @DepartmentId, @ProgrammingLanguageId);

                    SET @NewId = SCOPE_IDENTITY();

                    COMMIT TRANSACTION;

                    SET @IsSuccess = 1;
                    SELECT @NewId AS Id, '' AS ErrorMessage, CAST(@IsSuccess AS BIT) AS IsSuccess;
                END TRY
                BEGIN CATCH
                    IF @@TRANCOUNT > 0
                        ROLLBACK;

                    SET @IsSuccess = 0;
                    SELECT NULL AS Id, ERROR_MESSAGE() AS ErrorMessage, CAST(@IsSuccess AS BIT) AS IsSuccess;
                END CATCH
            END
            GO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetEmployeeById");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetEmployees");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS UpdateEmployee");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS CreateEmployee");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS DeleteEmployee");
        }
    }
}
