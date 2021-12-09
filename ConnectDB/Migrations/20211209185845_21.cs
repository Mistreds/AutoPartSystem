using Microsoft.EntityFrameworkCore.Migrations;

namespace ConnectDB.Migrations
{
    public partial class _21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoveGoods_MainMove_MainMoveId",
                table: "MoveGoods");

            migrationBuilder.DropColumn(
                name: "MainMove",
                table: "MoveGoods");

            migrationBuilder.AlterColumn<int>(
                name: "MainMoveId",
                table: "MoveGoods",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MoveGoods_MainMove_MainMoveId",
                table: "MoveGoods",
                column: "MainMoveId",
                principalTable: "MainMove",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoveGoods_MainMove_MainMoveId",
                table: "MoveGoods");

            migrationBuilder.AlterColumn<int>(
                name: "MainMoveId",
                table: "MoveGoods",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "MainMove",
                table: "MoveGoods",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_MoveGoods_MainMove_MainMoveId",
                table: "MoveGoods",
                column: "MainMoveId",
                principalTable: "MainMove",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
