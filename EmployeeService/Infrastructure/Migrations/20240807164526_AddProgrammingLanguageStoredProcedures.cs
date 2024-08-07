using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddProgrammingLanguageStoredProcedures : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE PROCEDURE GetProgrammingLanguageById
                    @Id INT
                AS
                BEGIN
                    SET NOCOUNT ON;
                    DECLARE @ErrorMessage NVARCHAR(4000);
                    DECLARE @IsSuccess BIT = 1;

                    BEGIN TRY
                        BEGIN TRANSACTION;

                        SELECT pl.Id,
                               pl.Name,
                               '' AS ErrorMessage,
                               CAST(@IsSuccess AS BIT) AS IsSuccess
                        FROM ProgrammingLanguages pl
                        WHERE pl.Id = @Id;

                        IF @@ROWCOUNT = 0
                        BEGIN
                            RAISERROR ('Programming Language with Id %d not found.', 16, 1, @Id);
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
                CREATE PROCEDURE GetProgrammingLanguages
                AS
                BEGIN
                    SET NOCOUNT ON;
                    DECLARE @ErrorMessage NVARCHAR(4000);
                    DECLARE @IsSuccess BIT = 1;

                    BEGIN TRY
                        BEGIN TRANSACTION;

                        SELECT pl.Id,
                               pl.Name,
                               '' AS ErrorMessage,
                               CAST(@IsSuccess AS BIT) AS IsSuccess
                        FROM ProgrammingLanguages pl;

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
                CREATE PROCEDURE UpdateProgrammingLanguage
                    @Id INT,
                    @Name NVARCHAR(50)
                AS
                BEGIN
                    SET NOCOUNT ON;
                    DECLARE @ErrorMessage NVARCHAR(4000);
                    DECLARE @IsSuccess BIT = 1;

                    BEGIN TRY
                        BEGIN TRANSACTION;

                        UPDATE ProgrammingLanguages
                        SET Name = @Name
                        WHERE Id = @Id;

                        IF @@ROWCOUNT = 0
                        BEGIN
                            RAISERROR ('Programming Language with Id %d not found.', 16, 1, @Id);
                        END
                        COMMIT TRANSACTION;

                        SET @IsSuccess = 1;
                        SELECT Id, '' AS ErrorMessage, CAST(@IsSuccess AS BIT) AS IsSuccess
                        FROM ProgrammingLanguages
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
                CREATE PROCEDURE CreateProgrammingLanguage
                    @Name NVARCHAR(50)
                AS
                BEGIN
                    SET NOCOUNT ON;
                    DECLARE @ErrorMessage NVARCHAR(4000);
                    DECLARE @IsSuccess BIT = 1;
                    DECLARE @NewId INT;

                    BEGIN TRY
                        BEGIN TRANSACTION;

                        INSERT INTO ProgrammingLanguages (Name)
                        VALUES (@Name);

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
                CREATE PROCEDURE DeleteProgrammingLanguage
                    @Id INT
                AS
                BEGIN
                    SET NOCOUNT ON;
                    DECLARE @ErrorMessage NVARCHAR(4000);
                    DECLARE @IsSuccess BIT = 1;

                    BEGIN TRY
                        BEGIN TRANSACTION;

                        DELETE FROM ProgrammingLanguages WHERE Id = @Id;

                        IF @@ROWCOUNT = 0
                        BEGIN
                            RAISERROR ('Programming Language with Id %d not found.', 16, 1, @Id);
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
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetProgrammingLanguageById");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetProgrammingLanguages");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS UpdateProgrammingLanguage");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS CreateProgrammingLanguage");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS DeleteProgrammingLanguage");
        }
    }
}
