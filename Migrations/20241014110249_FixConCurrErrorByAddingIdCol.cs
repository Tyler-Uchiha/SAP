using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SAP.Migrations
{
    /// <inheritdoc />
    public partial class FixConCurrErrorByAddingIdCol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Suspects_Table",
                table: "Suspects_Table");

            migrationBuilder.DeleteData(
                table: "Suspects_Table",
                keyColumn: "SuspectIdNum",
                keyValue: "0303011180187");

            migrationBuilder.DeleteData(
                table: "Suspects_Table",
                keyColumn: "SuspectIdNum",
                keyValue: "0303016812181");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Suspects_Table",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Suspects_Table",
                table: "Suspects_Table",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Suspects_Table",
                columns: new[] { "Id", "Age", "Eye_Color", "First_Name", "Height", "Last_Name", "Sex", "SuspectId", "SuspectIdNum" },
                values: new object[,]
                {
                    { 1, 0, "Green", "Andrew", 177.5m, "Dusk", "Male", null, "0303016812181" },
                    { 2, 0, "Brown", "Amanda", 158m, "White", "Female", null, "0303011180187" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Suspects_Table",
                table: "Suspects_Table");

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

            migrationBuilder.AddPrimaryKey(
                name: "PK_Suspects_Table",
                table: "Suspects_Table",
                column: "SuspectIdNum");

            migrationBuilder.InsertData(
                table: "Suspects_Table",
                columns: new[] { "SuspectIdNum", "Age", "Eye_Color", "First_Name", "Height", "Last_Name", "Sex", "SuspectId" },
                values: new object[,]
                {
                    { "0303011180187", 0, "Brown", "Amanda", 158m, "White", "Female", null },
                    { "0303016812181", 0, "Green", "Andrew", 177.5m, "Dusk", "Male", null }
                });
        }
    }
}
