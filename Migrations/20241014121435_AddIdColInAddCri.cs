using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SAP.Migrations
{
    /// <inheritdoc />
    public partial class AddIdColInAddCri : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CriminalRecords_Table",
                table: "CriminalRecords_Table");

            migrationBuilder.DeleteData(
                table: "CriminalRecords_Table",
                keyColumn: "CriminalRecordId",
                keyValue: "None");

            migrationBuilder.AlterColumn<string>(
                name: "CriminalRecordId",
                table: "CriminalRecords_Table",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "CriminalRecords_Table",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CriminalRecords_Table",
                table: "CriminalRecords_Table",
                column: "Id");

            migrationBuilder.InsertData(
                table: "CriminalRecords_Table",
                columns: new[] { "Id", "CriminalIdNum", "CriminalRecordId", "IssueDate", "IssuedAt", "IssuedBy", "OffenceCommitted", "Sentence" },
                values: new object[] { 1, "0303016812183", "None", "2024/10/10", "Sandton", "Kelvin Moores", "", 3 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CriminalRecords_Table",
                table: "CriminalRecords_Table");

            migrationBuilder.DeleteData(
                table: "CriminalRecords_Table",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CriminalRecords_Table");

            migrationBuilder.AlterColumn<string>(
                name: "CriminalRecordId",
                table: "CriminalRecords_Table",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CriminalRecords_Table",
                table: "CriminalRecords_Table",
                column: "CriminalRecordId");

            migrationBuilder.InsertData(
                table: "CriminalRecords_Table",
                columns: new[] { "CriminalRecordId", "CriminalIdNum", "IssueDate", "IssuedAt", "IssuedBy", "OffenceCommitted", "Sentence" },
                values: new object[] { "None", "0303016812183", "2024/10/10", "Sandton", "Kelvin Moores", "", 3 });
        }
    }
}
