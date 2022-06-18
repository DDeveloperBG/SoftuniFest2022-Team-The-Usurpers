#nullable disable

namespace App.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ChangedRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discounts_AspNetUsers_ShopkeeperId",
                table: "Discounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Terminals_AspNetUsers_ShopkeeperId",
                table: "Terminals");

            migrationBuilder.AddForeignKey(
                name: "FK_Discounts_Shopkeepers_ShopkeeperId",
                table: "Discounts",
                column: "ShopkeeperId",
                principalTable: "Shopkeepers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Terminals_Shopkeepers_ShopkeeperId",
                table: "Terminals",
                column: "ShopkeeperId",
                principalTable: "Shopkeepers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discounts_Shopkeepers_ShopkeeperId",
                table: "Discounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Terminals_Shopkeepers_ShopkeeperId",
                table: "Terminals");

            migrationBuilder.AddForeignKey(
                name: "FK_Discounts_AspNetUsers_ShopkeeperId",
                table: "Discounts",
                column: "ShopkeeperId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Terminals_AspNetUsers_ShopkeeperId",
                table: "Terminals",
                column: "ShopkeeperId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
