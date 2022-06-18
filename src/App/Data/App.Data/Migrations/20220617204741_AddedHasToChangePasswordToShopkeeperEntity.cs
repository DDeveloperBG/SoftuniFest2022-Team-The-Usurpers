#nullable disable

namespace App.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddedHasToChangePasswordToShopkeeperEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasToChangePassword",
                table: "Shopkeepers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasToChangePassword",
                table: "Shopkeepers");
        }
    }
}
