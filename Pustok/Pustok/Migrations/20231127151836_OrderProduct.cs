using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Pustok.Migrations
{
    public partial class OrderProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
               name: "OrderProducts",
               columns: table => new
               {
                   Id = table.Column<int>(type: "integer", nullable: false)
                       .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                   Quantity = table.Column<int>(type: "integer", nullable: false),
                   OrderId = table.Column<int>(type: "integer", nullable: false),
                   ProductId = table.Column<int>(type: "integer", nullable: false),
                   ColorId = table.Column<int>(type: "integer", nullable: false),
                   SizeId = table.Column<int>(type: "integer", nullable: false)
               },
               constraints: table =>
               {
                   table.PrimaryKey("PK_OrderProducts", x => x.Id);
                   table.ForeignKey(
                       name: "FK_OrderProducts_Colors_ColorId",
                       column: x => x.ColorId,
                       principalTable: "Colors",
                       principalColumn: "Id",
                       onDelete: ReferentialAction.Cascade);
                   table.ForeignKey(
                       name: "FK_OrderProducts_Orders_OrderId",
                       column: x => x.OrderId,
                       principalTable: "Orders",
                       principalColumn: "Id",
                       onDelete: ReferentialAction.Cascade);
                   table.ForeignKey(
                       name: "FK_OrderProducts_products_ProductId",
                       column: x => x.ProductId,
                       principalTable: "products",
                       principalColumn: "id",
                       onDelete: ReferentialAction.Cascade);
                   table.ForeignKey(
                       name: "FK_OrderProducts_Sizes_SizeId",
                       column: x => x.SizeId,
                       principalTable: "Sizes",
                       principalColumn: "Id",
                       onDelete: ReferentialAction.Cascade);
               });

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_ColorId",
                table: "OrderProducts",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_OrderId",
                table: "OrderProducts",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_ProductId",
                table: "OrderProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_SizeId",
                table: "OrderProducts",
                column: "SizeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
              name: "OrderProducts");
        }
    }
}
