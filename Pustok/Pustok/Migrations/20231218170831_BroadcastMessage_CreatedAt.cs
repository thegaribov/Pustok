using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pustok.Migrations
{
    public partial class BroadcastMessage_CreatedAt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_BroadcastMessage_BroadcastMessageId",
                table: "Notifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BroadcastMessage",
                table: "BroadcastMessage");

            migrationBuilder.RenameTable(
                name: "BroadcastMessage",
                newName: "BroadcastMessages");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "BroadcastMessages",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_BroadcastMessages",
                table: "BroadcastMessages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_BroadcastMessages_BroadcastMessageId",
                table: "Notifications",
                column: "BroadcastMessageId",
                principalTable: "BroadcastMessages",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_BroadcastMessages_BroadcastMessageId",
                table: "Notifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BroadcastMessages",
                table: "BroadcastMessages");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "BroadcastMessages");

            migrationBuilder.RenameTable(
                name: "BroadcastMessages",
                newName: "BroadcastMessage");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BroadcastMessage",
                table: "BroadcastMessage",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_BroadcastMessage_BroadcastMessageId",
                table: "Notifications",
                column: "BroadcastMessageId",
                principalTable: "BroadcastMessage",
                principalColumn: "Id");
        }
    }
}
