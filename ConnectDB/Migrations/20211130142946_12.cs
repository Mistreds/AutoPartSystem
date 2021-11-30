using Microsoft.EntityFrameworkCore.Migrations;

namespace ConnectDB.Migrations
{
    public partial class _12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.AddColumn<bool>(
                name: "IsVirtual",
                table: "Warehouse",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);  
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GoodsInvoice_Goods_GoodsId",
                table: "GoodsInvoice");

            migrationBuilder.DropColumn(
                name: "IsVirtual",
                table: "Warehouse");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "GoodsInvoice");

            migrationBuilder.AlterColumn<int>(
                name: "GoodsId",
                table: "GoodsInvoice",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "GoodId",
                table: "GoodsInvoice",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_GoodsInvoice_Goods_GoodsId",
                table: "GoodsInvoice",
                column: "GoodsId",
                principalTable: "Goods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
