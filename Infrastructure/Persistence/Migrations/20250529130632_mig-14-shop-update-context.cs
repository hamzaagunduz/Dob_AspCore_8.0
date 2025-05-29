using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig14shopupdatecontext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserShopItem_AspNetUsers_AppUserId",
                table: "UserShopItem");

            migrationBuilder.DropForeignKey(
                name: "FK_UserShopItem_ShopItems_ShopItemId",
                table: "UserShopItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserShopItem",
                table: "UserShopItem");

            migrationBuilder.RenameTable(
                name: "UserShopItem",
                newName: "UserShopItems");

            migrationBuilder.RenameIndex(
                name: "IX_UserShopItem_ShopItemId",
                table: "UserShopItems",
                newName: "IX_UserShopItems_ShopItemId");

            migrationBuilder.RenameIndex(
                name: "IX_UserShopItem_AppUserId",
                table: "UserShopItems",
                newName: "IX_UserShopItems_AppUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserShopItems",
                table: "UserShopItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserShopItems_AspNetUsers_AppUserId",
                table: "UserShopItems",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserShopItems_ShopItems_ShopItemId",
                table: "UserShopItems",
                column: "ShopItemId",
                principalTable: "ShopItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserShopItems_AspNetUsers_AppUserId",
                table: "UserShopItems");

            migrationBuilder.DropForeignKey(
                name: "FK_UserShopItems_ShopItems_ShopItemId",
                table: "UserShopItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserShopItems",
                table: "UserShopItems");

            migrationBuilder.RenameTable(
                name: "UserShopItems",
                newName: "UserShopItem");

            migrationBuilder.RenameIndex(
                name: "IX_UserShopItems_ShopItemId",
                table: "UserShopItem",
                newName: "IX_UserShopItem_ShopItemId");

            migrationBuilder.RenameIndex(
                name: "IX_UserShopItems_AppUserId",
                table: "UserShopItem",
                newName: "IX_UserShopItem_AppUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserShopItem",
                table: "UserShopItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserShopItem_AspNetUsers_AppUserId",
                table: "UserShopItem",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserShopItem_ShopItems_ShopItemId",
                table: "UserShopItem",
                column: "ShopItemId",
                principalTable: "ShopItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
