using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroceryShoppingCartAPI.Migrations
{
    public partial class UserCart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "userCarts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                table: "userCarts");
        }
    }
}
