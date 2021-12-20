using Microsoft.EntityFrameworkCore.Migrations;

namespace ConnectDB.Migrations
{
    public partial class _6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAgent",
                table: "Invoice",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAgent",
                table: "Client",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAgent",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "IsAgent",
                table: "Client");
        }
    }
}
