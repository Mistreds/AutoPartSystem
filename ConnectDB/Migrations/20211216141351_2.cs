using Microsoft.EntityFrameworkCore.Migrations;

namespace ConnectDB.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BrandId",
                table: "Goods",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Goods_BrandId",
                table: "Goods",
                column: "BrandId");

            migrationBuilder.AddForeignKey(
                name: "FK_Goods_Brand_BrandId",
                table: "Goods",
                column: "BrandId",
                principalTable: "Brand",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goods_Brand_BrandId",
                table: "Goods");

            migrationBuilder.DropIndex(
                name: "IX_Goods_BrandId",
                table: "Goods");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "Goods");
        }
    }
}
