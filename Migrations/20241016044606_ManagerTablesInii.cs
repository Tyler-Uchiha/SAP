using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SAP.Migrations
{
    /// <inheritdoc />
    public partial class ManagerTablesInii : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cases_Table",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CriminalRecordId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CriminalIdNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OffenceCommitted = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Sentence = table.Column<int>(type: "int", nullable: false),
                    IssuedAt = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IssuedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IssueDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoOfCases = table.Column<int>(type: "int", nullable: false),
                    IsAssigned = table.Column<bool>(type: "bit", nullable: true),
                    AssignedTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManagerId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cases_Table", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Managers_Table",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    First_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Last_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CaseCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Managers_Table", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cases_Table");

            migrationBuilder.DropTable(
                name: "Managers_Table");
        }
    }
}
