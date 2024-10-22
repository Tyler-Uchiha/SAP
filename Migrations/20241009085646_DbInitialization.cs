using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SAP.Migrations
{
    /// <inheritdoc />
    public partial class DbInitialization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Suspects_Table",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SuspectId = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    First_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Last_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Sex = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Eye_Color = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suspects_Table", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Suspects_Table",
                columns: new[] { "Id", "Age", "Eye_Color", "First_Name", "Height", "Last_Name", "Sex", "SuspectId" },
                values: new object[,]
                {
                    { 1, 0, "Green", "Andrew", 177.5m, "Dusk", "Male", "0303016812181" },
                    { 2, 0, "Brown", "Amanda", 158m, "White", "Female", "0303011180187" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Suspects_Table");
        }
    }
}
