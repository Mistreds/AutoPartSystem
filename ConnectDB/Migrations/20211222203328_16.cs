using Microsoft.EntityFrameworkCore.Migrations;

namespace ConnectDB.Migrations
{
    public partial class _16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DownGoods",
                table: "Employee",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "Employee",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SetCell",
                table: "Employee",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SetEditGood",
                table: "Employee",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SetGood",
                table: "Employee",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SetMoveAgent",
                table: "Employee",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SetMoveCity",
                table: "Employee",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SetPrihod",
                table: "Employee",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SetReport",
                table: "Employee",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SetStat",
                table: "Employee",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DownGoods",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "SetCell",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "SetEditGood",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "SetGood",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "SetMoveAgent",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "SetMoveCity",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "SetPrihod",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "SetReport",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "SetStat",
                table: "Employee");
        }
    }
}
