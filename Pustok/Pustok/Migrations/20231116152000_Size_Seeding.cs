using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pustok.Migrations
{
    public partial class Size_Seeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Sizes",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.InsertData(
                table: "Sizes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { -5, "XL" },
                    { -4, "L" },
                    { -3, "XXS" },
                    { -2, "XS" },
                    { -1, "S" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: -5);

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: -4);

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: -3);

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "Sizes",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "Sizes",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
