using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SAP.Migrations
{
    /// <inheritdoc />
    public partial class ManagerTablesIniv : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "CriminalRecords_Table",
                keyColumn: "Id",
                keyValue: 1,
                column: "OffenceCommitted",
                value: "Drugs");

            migrationBuilder.InsertData(
                table: "Managers_Table",
                columns: new[] { "Id", "CaseCount", "First_Name", "Last_Name" },
                values: new object[,]
                {
                    { 1, 3, "Johnny", "Bravo" },
                    { 2, 0, "Jill", "Scott" },
                    { 3, 7, "Laura", "Croft" },
                    { 4, 3, "Angel", "Jolls" },
                    { 5, 2, "Tom", "Crew" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Managers_Table",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Managers_Table",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Managers_Table",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Managers_Table",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Managers_Table",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "CriminalRecords_Table",
                keyColumn: "Id",
                keyValue: 1,
                column: "OffenceCommitted",
                value: "");
        }
    }
}
