using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ConnectDB.Migrations
{
    public partial class _114 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TypePayId",
                table: "GoodsInvoice",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TypePay",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypePay", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Менеджер");

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Региональный менеджер");

            migrationBuilder.InsertData(
                table: "TypePay",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Наличная оплата" },
                    { 2, "Карта" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TypePay");

            migrationBuilder.DropColumn(
                name: "TypePayId",
                table: "GoodsInvoice");

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Продажник");

            migrationBuilder.UpdateData(
                table: "Position",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "Продажник регионал");
        }
    }
}
