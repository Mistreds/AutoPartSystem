using Microsoft.EntityFrameworkCore.Migrations;

namespace ConnectDB.Migrations
{
    public partial class _4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.AddColumn<bool>(
                name: "IsInvoice",
                table: "Invoice",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsInvoice",
                table: "Invoice");

            migrationBuilder.AddColumn<double>(
                name: "TransPrice",
                table: "GoodsInvoice",
                type: "double",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
