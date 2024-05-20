using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBoardsApp.Migrations
{
    /// <inheritdoc />
    public partial class ViewTopAuthorsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                CREATE VIEW View_TopAuthors AS
                SELECT TOP 5 u.FullName, COUNT(*) AS WorkItemsCreated
                FROM Users u
                JOIN WorkItems w ON u.UserId = w.AuthorId
                GROUP BY u.UserId, u.FullName
                ORDER BY WorkItemsCreated DESC;
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                                 DROP VIEW View_TopAuthors;
                                 """);
        }
    }
}
