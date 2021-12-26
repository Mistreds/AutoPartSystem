using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ConnectDB.Migrations
{
    public partial class _14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BackInvoice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    GoodsInvoiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BackInvoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BackInvoice_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BackInvoice_GoodsInvoice_GoodsInvoiceId",
                        column: x => x.GoodsInvoiceId,
                        principalTable: "GoodsInvoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "TypeExpenses",
                columns: new[] { "Id", "Name" },
                values: new object[] { 5, "Возврат" });

            migrationBuilder.CreateIndex(
                name: "IX_BackInvoice_EmployeeId",
                table: "BackInvoice",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_BackInvoice_GoodsInvoiceId",
                table: "BackInvoice",
                column: "GoodsInvoiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BackInvoice");

            migrationBuilder.DeleteData(
                table: "TypeExpenses",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
