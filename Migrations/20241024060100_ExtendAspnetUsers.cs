using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SAP.Migrations
{
    /// <inheritdoc />
    public partial class ExtendAspnetUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "No_of_Cases",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "User_Role",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "User_Role_Identifier",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "No_of_Cases",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "User_Role",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "User_Role_Identifier",
                table: "AspNetUsers");
        }
    }
}
