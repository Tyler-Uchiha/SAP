using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SAP.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedTableCols : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Age",
                table: "CriminalRecords_Table",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "First_Name",
                table: "CriminalRecords_Table",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Last_Name",
                table: "CriminalRecords_Table",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "CriminalRecords_Table",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Age", "First_Name", "Last_Name" },
                values: new object[] { null, null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "CriminalRecords_Table");

            migrationBuilder.DropColumn(
                name: "First_Name",
                table: "CriminalRecords_Table");

            migrationBuilder.DropColumn(
                name: "Last_Name",
                table: "CriminalRecords_Table");
        }
    }
}
