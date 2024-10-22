using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SAP.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedDatetimeRec : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CriminalRecords_Table",
                columns: new[] { "Id", "IssueDate", "IssuedAt", "IssuedBy", "OffenceCommitted", "Sentence" },
                values: new object[] { 1, new DateTime(2024, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sandton", "Kelvin Moores", "", 3 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CriminalRecords_Table",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
