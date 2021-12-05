using Microsoft.EntityFrameworkCore.Migrations;

namespace ConnectDB.Migrations
{
    public partial class _115 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "AllInputPrice",
                table: "Invoice",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "InputAstana",
                table: "Goods",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Input_aktau",
                table: "Goods",
                type: "double",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllInputPrice",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "InputAstana",
                table: "Goods");

            migrationBuilder.DropColumn(
                name: "Input_aktau",
                table: "Goods");
        }
    }
}
