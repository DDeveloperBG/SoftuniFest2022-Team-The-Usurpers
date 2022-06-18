#nullable disable

namespace App.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ChangedRelationOfDiscountVote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiscountsVotes_BankEmployees_BankEmployeeId",
                table: "DiscountsVotes");

            migrationBuilder.RenameColumn(
                name: "BankEmployeeId",
                table: "DiscountsVotes",
                newName: "BankEmployeeUserId");

            migrationBuilder.RenameIndex(
                name: "IX_DiscountsVotes_BankEmployeeId",
                table: "DiscountsVotes",
                newName: "IX_DiscountsVotes_BankEmployeeUserId");

            migrationBuilder.AlterColumn<short>(
                name: "Vote",
                table: "DiscountsVotes",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.AddForeignKey(
                name: "FK_DiscountsVotes_AspNetUsers_BankEmployeeUserId",
                table: "DiscountsVotes",
                column: "BankEmployeeUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiscountsVotes_AspNetUsers_BankEmployeeUserId",
                table: "DiscountsVotes");

            migrationBuilder.RenameColumn(
                name: "BankEmployeeUserId",
                table: "DiscountsVotes",
                newName: "BankEmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_DiscountsVotes_BankEmployeeUserId",
                table: "DiscountsVotes",
                newName: "IX_DiscountsVotes_BankEmployeeId");

            migrationBuilder.AlterColumn<byte>(
                name: "Vote",
                table: "DiscountsVotes",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AddForeignKey(
                name: "FK_DiscountsVotes_BankEmployees_BankEmployeeId",
                table: "DiscountsVotes",
                column: "BankEmployeeId",
                principalTable: "BankEmployees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
