using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SAP.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserCaseCountCol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "No_of_Cases",
                table: "AspNetUsers",
                newName: "CaseCount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CaseCount",
                table: "AspNetUsers",
                newName: "No_of_Cases");
        }
    }
}
