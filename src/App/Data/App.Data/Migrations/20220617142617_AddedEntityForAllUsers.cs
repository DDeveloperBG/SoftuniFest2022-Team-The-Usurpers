using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Data.Migrations
{
    public partial class AddedEntityForAllUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardHolders_AspNetUsers_ApplicationUserId",
                table: "CardHolders");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "CardHolders",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CardHolders_ApplicationUserId",
                table: "CardHolders",
                newName: "IX_CardHolders_UserId");

            migrationBuilder.AddColumn<DateTime>(
                name: "RegisteredOn",
                table: "CardHolders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "BankEmployees",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankEmployees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankEmployees_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Shopkeepers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RegisteredOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shopkeepers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shopkeepers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BankEmployees_UserId",
                table: "BankEmployees",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Shopkeepers_UserId",
                table: "Shopkeepers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CardHolders_AspNetUsers_UserId",
                table: "CardHolders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardHolders_AspNetUsers_UserId",
                table: "CardHolders");

            migrationBuilder.DropTable(
                name: "BankEmployees");

            migrationBuilder.DropTable(
                name: "Shopkeepers");

            migrationBuilder.DropColumn(
                name: "RegisteredOn",
                table: "CardHolders");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "CardHolders",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_CardHolders_UserId",
                table: "CardHolders",
                newName: "IX_CardHolders_ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CardHolders_AspNetUsers_ApplicationUserId",
                table: "CardHolders",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
