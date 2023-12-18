using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Pustok.Migrations
{
    public partial class BroadcastMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BroadcastMessageId",
                table: "Notifications",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BroadcastMessage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Message = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BroadcastMessage", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_BroadcastMessageId",
                table: "Notifications",
                column: "BroadcastMessageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_BroadcastMessage_BroadcastMessageId",
                table: "Notifications",
                column: "BroadcastMessageId",
                principalTable: "BroadcastMessage",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_BroadcastMessage_BroadcastMessageId",
                table: "Notifications");

            migrationBuilder.DropTable(
                name: "BroadcastMessage");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_BroadcastMessageId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "BroadcastMessageId",
                table: "Notifications");
        }
    }
}
