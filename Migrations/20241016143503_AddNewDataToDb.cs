using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SAP.Migrations
{
    /// <inheritdoc />
    public partial class AddNewDataToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cases_Table",
                columns: new[] { "Id", "AssignedTo", "CriminalIdNum", "CriminalRecordId", "IsAssigned", "IssueDate", "IssuedAt", "IssuedBy", "ManagerId", "OffenceCommitted", "Sentence" },
                values: new object[] { 1, "None", "1111111231234", "C-1111-20241015-9999", false, "2024-10-10", "Sandton", "Kelvin Moores", "None", "Drugs", 3 });

            migrationBuilder.UpdateData(
                table: "CriminalRecords_Table",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CriminalIdNum", "CriminalRecordId", "IssueDate" },
                values: new object[] { "1111111231234", "C-1111-20241015-9999", "2024-10-10" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cases_Table",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "CriminalRecords_Table",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CriminalIdNum", "CriminalRecordId", "IssueDate" },
                values: new object[] { "0303016812183", "None", "2024/10/10" });
        }
    }
}
