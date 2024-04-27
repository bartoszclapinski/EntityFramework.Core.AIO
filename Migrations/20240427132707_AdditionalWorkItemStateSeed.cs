using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBoardsApp.Migrations
{
    /// <inheritdoc />
    public partial class AdditionalWorkItemStateSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "WorkItemStates",
                columns: new[] { "WorkItemStateId", "Value" },
                values: new object[] { 4, "In Review" }
                );
            
            migrationBuilder.InsertData(
                table: "WorkItemStates",
                columns: new[] { "WorkItemStateId", "Value" },
                values: new object[] {5, "Rejected" }
                );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "WorkItemStates",
                keyColumn: "WorkItemStateId",
                keyValue: 4
                );
            
            migrationBuilder.DeleteData(
                table: "WorkItemStates",
                keyColumn: "WorkItemStateId",
                keyValue: 5
                );
        }
    }
}
