#nullable disable

namespace App.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddedDiscountVoteEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankEmployees_AspNetUsers_UserId",
                table: "BankEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_CardHolders_AspNetUsers_UserId",
                table: "CardHolders");

            migrationBuilder.DropForeignKey(
                name: "FK_Discounts_Shopkeepers_ShopkeeperId",
                table: "Discounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Shopkeepers_AspNetUsers_UserId",
                table: "Shopkeepers");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Shopkeepers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: string.Empty,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ShopkeeperId",
                table: "Discounts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: string.Empty,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "CardHolders",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: string.Empty,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "BankEmployees",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: string.Empty,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "DiscountsVotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Vote = table.Column<byte>(type: "tinyint", nullable: false),
                    DiscountId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BankEmployeeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountsVotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiscountsVotes_BankEmployees_BankEmployeeId",
                        column: x => x.BankEmployeeId,
                        principalTable: "BankEmployees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DiscountsVotes_Discounts_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "Discounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiscountsVotes_BankEmployeeId",
                table: "DiscountsVotes",
                column: "BankEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscountsVotes_DiscountId",
                table: "DiscountsVotes",
                column: "DiscountId");

            migrationBuilder.AddForeignKey(
                name: "FK_BankEmployees_AspNetUsers_UserId",
                table: "BankEmployees",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CardHolders_AspNetUsers_UserId",
                table: "CardHolders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Discounts_Shopkeepers_ShopkeeperId",
                table: "Discounts",
                column: "ShopkeeperId",
                principalTable: "Shopkeepers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Shopkeepers_AspNetUsers_UserId",
                table: "Shopkeepers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankEmployees_AspNetUsers_UserId",
                table: "BankEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_CardHolders_AspNetUsers_UserId",
                table: "CardHolders");

            migrationBuilder.DropForeignKey(
                name: "FK_Discounts_Shopkeepers_ShopkeeperId",
                table: "Discounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Shopkeepers_AspNetUsers_UserId",
                table: "Shopkeepers");

            migrationBuilder.DropTable(
                name: "DiscountsVotes");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Shopkeepers",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ShopkeeperId",
                table: "Discounts",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "CardHolders",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "BankEmployees",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_BankEmployees_AspNetUsers_UserId",
                table: "BankEmployees",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CardHolders_AspNetUsers_UserId",
                table: "CardHolders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Discounts_Shopkeepers_ShopkeeperId",
                table: "Discounts",
                column: "ShopkeeperId",
                principalTable: "Shopkeepers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Shopkeepers_AspNetUsers_UserId",
                table: "Shopkeepers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
