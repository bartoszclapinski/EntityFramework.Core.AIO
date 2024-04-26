using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyBoardsApp.Migrations
{
    /// <inheritdoc />
    public partial class WorkItemStateDataSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "WorkItemStates",
                columns: new[] { "WorkItemStateId", "Value" },
                values: new object[,]
                {
                    { 1, "To Do" },
                    { 2, "In Progress" },
                    { 3, "Done" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "WorkItemStates",
                keyColumn: "WorkItemStateId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "WorkItemStates",
                keyColumn: "WorkItemStateId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "WorkItemStates",
                keyColumn: "WorkItemStateId",
                keyValue: 3);
        }
    }
}
