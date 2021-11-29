using Microsoft.EntityFrameworkCore.Migrations;

namespace ConnectDB.Migrations
{
    public partial class _5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ModelId",
                table: "GoodsInvoice",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_GoodsInvoice_ModelId",
                table: "GoodsInvoice",
                column: "ModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_GoodsInvoice_Model_ModelId",
                table: "GoodsInvoice",
                column: "ModelId",
                principalTable: "Model",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GoodsInvoice_Model_ModelId",
                table: "GoodsInvoice");

            migrationBuilder.DropIndex(
                name: "IX_GoodsInvoice_ModelId",
                table: "GoodsInvoice");

            migrationBuilder.DropColumn(
                name: "ModelId",
                table: "GoodsInvoice");
        }
    }
}
