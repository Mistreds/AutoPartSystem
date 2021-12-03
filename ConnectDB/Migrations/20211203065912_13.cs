using Microsoft.EntityFrameworkCore.Migrations;

namespace ConnectDB.Migrations
{
    public partial class _13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "InputPrice",
                table: "GoodsInvoice",
                type: "double",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "RecomPrice",
                table: "GoodsInvoice",
                type: "double",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InputPrice",
                table: "GoodsInvoice");

            migrationBuilder.DropColumn(
                name: "RecomPrice",
                table: "GoodsInvoice");
        }
    }
}
