using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SAP.Migrations
{
    /// <inheritdoc />
    public partial class ManagerTablesInv : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ManagerId",
                table: "Managers_Table",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Managers_Table",
                keyColumn: "Id",
                keyValue: 1,
                column: "ManagerId",
                value: "M-1");

            migrationBuilder.UpdateData(
                table: "Managers_Table",
                keyColumn: "Id",
                keyValue: 2,
                column: "ManagerId",
                value: "M-2");

            migrationBuilder.UpdateData(
                table: "Managers_Table",
                keyColumn: "Id",
                keyValue: 3,
                column: "ManagerId",
                value: "M-3");

            migrationBuilder.UpdateData(
                table: "Managers_Table",
                keyColumn: "Id",
                keyValue: 4,
                column: "ManagerId",
                value: "M-4");

            migrationBuilder.UpdateData(
                table: "Managers_Table",
                keyColumn: "Id",
                keyValue: 5,
                column: "ManagerId",
                value: "M-5");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "Managers_Table");
        }
    }
}
