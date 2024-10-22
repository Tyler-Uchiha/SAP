using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SAP.Migrations
{
    /// <inheritdoc />
    public partial class SetUpMultTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CriminalRecords_Table",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OffenceCommitted = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Sentence = table.Column<int>(type: "int", maxLength: 4, nullable: false),
                    IssuedAt = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IssuedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CriminalRecords_Table", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Offences_Table",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Offences = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OffenceCode = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offences_Table", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Offences_Table",
                columns: new[] { "Id", "OffenceCode", "Offences" },
                values: new object[,]
                {
                    { 1, "A1", "Assault" },
                    { 2, "A2", "Burglary" },
                    { 3, "A3", "Drugs" },
                    { 4, "A4", "Hijacking" },
                    { 5, "A5", "Murder" },
                    { 6, "A6", "Offences of Dishonesty" },
                    { 7, "A7", "Other offences" },
                    { 8, "A8", "Public drinking" },
                    { 9, "A9", "Rape" },
                    { 10, "A10", "Robbery" },
                    { 11, "A11", "Sexual Harrassment" },
                    { 12, "A12", "Sexual Offences" },
                    { 13, "A13", "Violence" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CriminalRecords_Table");

            migrationBuilder.DropTable(
                name: "Offences_Table");
        }
    }
}
