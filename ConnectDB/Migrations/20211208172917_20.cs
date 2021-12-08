using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ConnectDB.Migrations
{
    public partial class _20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MainMove",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    CityFromId = table.Column<int>(type: "int", nullable: false),
                    CityToId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainMove", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MainMove_City_CityFromId",
                        column: x => x.CityFromId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MainMove_City_CityToId",
                        column: x => x.CityToId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MainMove_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MoveGoods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    WarehouseId = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false),
                    MainMove = table.Column<int>(type: "int", nullable: false),
                    MainMoveId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoveGoods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MoveGoods_MainMove_MainMoveId",
                        column: x => x.MainMoveId,
                        principalTable: "MainMove",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MoveGoods_Warehouse_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_MainMove_CityFromId",
                table: "MainMove",
                column: "CityFromId");

            migrationBuilder.CreateIndex(
                name: "IX_MainMove_CityToId",
                table: "MainMove",
                column: "CityToId");

            migrationBuilder.CreateIndex(
                name: "IX_MainMove_EmployeeId",
                table: "MainMove",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_MoveGoods_MainMoveId",
                table: "MoveGoods",
                column: "MainMoveId");

            migrationBuilder.CreateIndex(
                name: "IX_MoveGoods_WarehouseId",
                table: "MoveGoods",
                column: "WarehouseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MoveGoods");

            migrationBuilder.DropTable(
                name: "MainMove");
        }
    }
}
