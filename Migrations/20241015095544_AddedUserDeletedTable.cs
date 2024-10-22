using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SAP.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserDeletedTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "userDeletedCriminals_Table",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CriminalRecordId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CriminalIdNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OffenceCommitted = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Sentence = table.Column<int>(type: "int", nullable: false),
                    IssuedAt = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IssuedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IssueDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OriginalId = table.Column<int>(type: "int", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    User_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DesktopUser = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userDeletedCriminals_Table", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "userDeletedCriminals_Table");
        }
    }
}
