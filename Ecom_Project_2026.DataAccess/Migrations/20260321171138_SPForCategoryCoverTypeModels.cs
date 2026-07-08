using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecom_Project_2026.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SPForCategoryCoverTypeModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // =========================
            // Category Stored Procedures
            // =========================
            migrationBuilder.Sql(@"
                CREATE PROCEDURE GetCategories
                AS
                BEGIN
                    SET NOCOUNT ON;
                    SELECT * FROM Categories;
                END
            ");

            migrationBuilder.Sql(@"
                CREATE PROCEDURE GetCategory
                    @Id INT
                AS
                BEGIN
                    SET NOCOUNT ON;
                    SELECT * FROM Categories WHERE Id = @Id;
                END
            ");

            migrationBuilder.Sql(@"
                CREATE PROCEDURE CreateCategory
                    @Name NVARCHAR(100)
                AS
                BEGIN
                    SET NOCOUNT ON;
                    INSERT INTO Categories (Name)
                    VALUES (@Name);
                END
            ");

            migrationBuilder.Sql(@"
                CREATE PROCEDURE UpdateCategory
                    @Id INT,
                    @Name NVARCHAR(100)
                AS
                BEGIN
                    SET NOCOUNT ON;
                    UPDATE Categories
                    SET Name = @Name
                    WHERE Id = @Id;
                END
            ");

            migrationBuilder.Sql(@"
                CREATE PROCEDURE DeleteCategory
                    @Id INT
                AS
                BEGIN
                    SET NOCOUNT ON;
                    DELETE FROM Categories
                    WHERE Id = @Id;
                END
            ");

            // =========================
            // CoverType Stored Procedures
            // =========================
            migrationBuilder.Sql(@"
                CREATE PROCEDURE GetCoverTypes
                AS
                BEGIN
                    SET NOCOUNT ON;
                    SELECT * FROM CoverTypes;
                END
            ");

            migrationBuilder.Sql(@"
                CREATE PROCEDURE GetCoverType
                    @Id INT
                AS
                BEGIN
                    SET NOCOUNT ON;
                    SELECT * FROM CoverTypes WHERE Id = @Id;
                END
            ");

            migrationBuilder.Sql(@"
                CREATE PROCEDURE CreateCoverType
                    @Name NVARCHAR(100)
                AS
                BEGIN
                    SET NOCOUNT ON;
                    INSERT INTO CoverTypes (Name)
                    VALUES (@Name);
                END
            ");

            migrationBuilder.Sql(@"
                CREATE PROCEDURE UpdateCoverType
                    @Id INT,
                    @Name NVARCHAR(100)
                AS
                BEGIN
                    SET NOCOUNT ON;
                    UPDATE CoverTypes
                    SET Name = @Name
                    WHERE Id = @Id;
                END
            ");

            migrationBuilder.Sql(@"
                CREATE PROCEDURE DeleteCoverType
                    @Id INT
                AS
                BEGIN
                    SET NOCOUNT ON;
                    DELETE FROM CoverTypes
                    WHERE Id = @Id;
                END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetCategories");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetCategory");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS CreateCategory");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS UpdateCategory");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS DeleteCategory");

            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetCoverTypes");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetCoverType");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS CreateCoverType");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS UpdateCoverType");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS DeleteCoverType");
        }
    }
}