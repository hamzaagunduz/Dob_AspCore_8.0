using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class userflash : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserFlashCard_AspNetUsers_AppUserID",
                table: "AppUserFlashCard");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUserFlashCard_FlashCards_FlashCardID",
                table: "AppUserFlashCard");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppUserFlashCard",
                table: "AppUserFlashCard");

            migrationBuilder.RenameTable(
                name: "AppUserFlashCard",
                newName: "AppUserFlashCards");

            migrationBuilder.RenameIndex(
                name: "IX_AppUserFlashCard_FlashCardID",
                table: "AppUserFlashCards",
                newName: "IX_AppUserFlashCards_FlashCardID");

            migrationBuilder.RenameIndex(
                name: "IX_AppUserFlashCard_AppUserID",
                table: "AppUserFlashCards",
                newName: "IX_AppUserFlashCards_AppUserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppUserFlashCards",
                table: "AppUserFlashCards",
                column: "AppUserFlashCardID");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserFlashCards_AspNetUsers_AppUserID",
                table: "AppUserFlashCards",
                column: "AppUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserFlashCards_FlashCards_FlashCardID",
                table: "AppUserFlashCards",
                column: "FlashCardID",
                principalTable: "FlashCards",
                principalColumn: "FlashCardID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserFlashCards_AspNetUsers_AppUserID",
                table: "AppUserFlashCards");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUserFlashCards_FlashCards_FlashCardID",
                table: "AppUserFlashCards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppUserFlashCards",
                table: "AppUserFlashCards");

            migrationBuilder.RenameTable(
                name: "AppUserFlashCards",
                newName: "AppUserFlashCard");

            migrationBuilder.RenameIndex(
                name: "IX_AppUserFlashCards_FlashCardID",
                table: "AppUserFlashCard",
                newName: "IX_AppUserFlashCard_FlashCardID");

            migrationBuilder.RenameIndex(
                name: "IX_AppUserFlashCards_AppUserID",
                table: "AppUserFlashCard",
                newName: "IX_AppUserFlashCard_AppUserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppUserFlashCard",
                table: "AppUserFlashCard",
                column: "AppUserFlashCardID");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserFlashCard_AspNetUsers_AppUserID",
                table: "AppUserFlashCard",
                column: "AppUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserFlashCard_FlashCards_FlashCardID",
                table: "AppUserFlashCard",
                column: "FlashCardID",
                principalTable: "FlashCards",
                principalColumn: "FlashCardID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
