using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SAP.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusToCriminalRec : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Case_Status",
                table: "CriminalRecords_Table",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "CriminalRecords_Table",
                keyColumn: "Id",
                keyValue: 1,
                column: "Case_Status",
                value: "Not Started");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Case_Status",
                table: "CriminalRecords_Table");
        }
    }
}
