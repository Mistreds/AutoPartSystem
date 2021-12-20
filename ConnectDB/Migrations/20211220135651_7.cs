using Microsoft.EntityFrameworkCore.Migrations;

namespace ConnectDB.Migrations
{
    public partial class _7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Mark",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "" });

            migrationBuilder.InsertData(
                table: "Model",
                columns: new[] { "Id", "MarkId", "Name" },
                values: new object[] { 1, 1, "" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Model",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Mark",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
