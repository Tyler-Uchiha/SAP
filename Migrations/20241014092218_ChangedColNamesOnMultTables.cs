using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SAP.Migrations
{
    /// <inheritdoc />
    public partial class ChangedColNamesOnMultTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Suspects_Table",
                table: "Suspects_Table");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CriminalRecords_Table",
                table: "CriminalRecords_Table");

            migrationBuilder.DeleteData(
                table: "CriminalRecords_Table",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Suspects_Table",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Suspects_Table",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Suspects_Table");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CriminalRecords_Table");

            migrationBuilder.AlterColumn<string>(
                name: "SuspectId",
                table: "Suspects_Table",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(13)",
                oldMaxLength: 13);

            migrationBuilder.AddColumn<string>(
                name: "SuspectIdNum",
                table: "Suspects_Table",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CriminalRecordId",
                table: "CriminalRecords_Table",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CriminalIdNum",
                table: "CriminalRecords_Table",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Suspects_Table",
                table: "Suspects_Table",
                column: "SuspectIdNum");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CriminalRecords_Table",
                table: "CriminalRecords_Table",
                column: "CriminalRecordId");

            migrationBuilder.InsertData(
                table: "CriminalRecords_Table",
                columns: new[] { "CriminalRecordId", "CriminalIdNum", "IssueDate", "IssuedAt", "IssuedBy", "OffenceCommitted", "Sentence" },
                values: new object[] { "None", "0303016812183", "2024/10/10", "Sandton", "Kelvin Moores", "", 3 });

            migrationBuilder.InsertData(
                table: "Suspects_Table",
                columns: new[] { "SuspectIdNum", "Age", "Eye_Color", "First_Name", "Height", "Last_Name", "Sex", "SuspectId" },
                values: new object[,]
                {
                    { "0303011180187", 0, "Brown", "Amanda", 158m, "White", "Female", null },
                    { "0303016812181", 0, "Green", "Andrew", 177.5m, "Dusk", "Male", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Suspects_Table",
                table: "Suspects_Table");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CriminalRecords_Table",
                table: "CriminalRecords_Table");

            migrationBuilder.DeleteData(
                table: "CriminalRecords_Table",
                keyColumn: "CriminalRecordId",
                keyColumnType: "nvarchar(450)",
                keyValue: "None");

            migrationBuilder.DeleteData(
                table: "Suspects_Table",
                keyColumn: "SuspectIdNum",
                keyColumnType: "nvarchar(13)",
                keyValue: "0303011180187");

            migrationBuilder.DeleteData(
                table: "Suspects_Table",
                keyColumn: "SuspectIdNum",
                keyColumnType: "nvarchar(13)",
                keyValue: "0303016812181");

            migrationBuilder.DropColumn(
                name: "SuspectIdNum",
                table: "Suspects_Table");

            migrationBuilder.DropColumn(
                name: "CriminalRecordId",
                table: "CriminalRecords_Table");

            migrationBuilder.DropColumn(
                name: "CriminalIdNum",
                table: "CriminalRecords_Table");

            migrationBuilder.AlterColumn<string>(
                name: "SuspectId",
                table: "Suspects_Table",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Suspects_Table",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "CriminalRecords_Table",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Suspects_Table",
                table: "Suspects_Table",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CriminalRecords_Table",
                table: "CriminalRecords_Table",
                column: "Id");

            migrationBuilder.InsertData(
                table: "CriminalRecords_Table",
                columns: new[] { "Id", "IssueDate", "IssuedAt", "IssuedBy", "OffenceCommitted", "Sentence" },
                values: new object[] { 1, "2024/10/10", "Sandton", "Kelvin Moores", "", 3 });

            migrationBuilder.InsertData(
                table: "Suspects_Table",
                columns: new[] { "Id", "Age", "Eye_Color", "First_Name", "Height", "Last_Name", "Sex", "SuspectId" },
                values: new object[,]
                {
                    { 1, 0, "Green", "Andrew", 177.5m, "Dusk", "Male", "0303016812181" },
                    { 2, 0, "Brown", "Amanda", 158m, "White", "Female", "0303011180187" }
                });
        }
    }
}
