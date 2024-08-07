using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddDepartmentStoredProcedures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE PROCEDURE GetDepartmentById
                    @Id INT
                AS
                BEGIN
                    SET NOCOUNT ON;
                    DECLARE @ErrorMessage NVARCHAR(4000);
                    DECLARE @IsSuccess BIT = 1;

                    BEGIN TRY
                        BEGIN TRANSACTION;

                        SELECT d.Id,
                               d.Name,
                               d.Floor,
                               '' AS ErrorMessage,
                               CAST(@IsSuccess AS BIT) AS IsSuccess
                        FROM Departments d
                        WHERE d.Id = @Id;

                        IF @@ROWCOUNT = 0
                        BEGIN
                            RAISERROR ('Department with Id %d not found.', 16, 1, @Id);
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

            migrationBuilder.Sql(@"
                CREATE PROCEDURE GetDepartments
                AS
                BEGIN
                    SET NOCOUNT ON;
                    DECLARE @ErrorMessage NVARCHAR(4000);
                    DECLARE @IsSuccess BIT = 1;

                    BEGIN TRY
                        BEGIN TRANSACTION;

                        SELECT d.Id,
                               d.Name,
                               d.Floor,
                               '' AS ErrorMessage,
                               CAST(@IsSuccess AS BIT) AS IsSuccess
                        FROM Departments d;

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

            migrationBuilder.Sql(@"
                CREATE PROCEDURE UpdateDepartment
                    @Id INT,
                    @Name NVARCHAR(50),
                    @Floor INT
                AS
                BEGIN
                    SET NOCOUNT ON;
                    DECLARE @ErrorMessage NVARCHAR(4000);
                    DECLARE @IsSuccess BIT = 1;

                    BEGIN TRY
                        BEGIN TRANSACTION;

                        UPDATE Departments
                        SET Name = @Name,
                            Floor = @Floor
                        WHERE Id = @Id;

                        IF @@ROWCOUNT = 0
                        BEGIN
                            RAISERROR ('Department with Id %d not found.', 16, 1, @Id);
                        END
                        COMMIT TRANSACTION;

                        SET @IsSuccess = 1;
                        SELECT Id, '' AS ErrorMessage, CAST(@IsSuccess AS BIT) AS IsSuccess
                        FROM Departments
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

            migrationBuilder.Sql(@"
                CREATE PROCEDURE CreateDepartment
                    @Name NVARCHAR(50),
                    @Floor INT
                AS
                BEGIN
                    SET NOCOUNT ON;
                    DECLARE @ErrorMessage NVARCHAR(4000);
                    DECLARE @IsSuccess BIT = 1;
                    DECLARE @NewId INT;

                    BEGIN TRY
                        BEGIN TRANSACTION;

                        INSERT INTO Departments (Name, Floor)
                        VALUES (@Name, @Floor);

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

            migrationBuilder.Sql(@"
                CREATE PROCEDURE DeleteDepartment
                    @Id INT
                AS
                BEGIN
                    SET NOCOUNT ON;
                    DECLARE @ErrorMessage NVARCHAR(4000);
                    DECLARE @IsSuccess BIT = 1;

                    BEGIN TRY
                        BEGIN TRANSACTION;

                        DELETE FROM Departments WHERE Id = @Id;

                        IF @@ROWCOUNT = 0
                        BEGIN
                            RAISERROR ('Department with Id %d not found.', 16, 1, @Id);
                        END

                        SET @IsSuccess = 1;
                        SELECT 0 AS Id, '' AS ErrorMessage, CAST(@IsSuccess AS BIT) AS IsSuccess;

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetDepartmentById");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetDepartments");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS UpdateDepartment");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS CreateDepartment");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS DeleteDepartment");
        }
    }
}
