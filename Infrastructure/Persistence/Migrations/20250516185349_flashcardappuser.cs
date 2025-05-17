using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class flashcardappuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserFlashCard_AspNetUsers_UsersId",
                table: "AppUserFlashCard");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUserFlashCard_FlashCards_FlashCardsFlashCardID",
                table: "AppUserFlashCard");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppUserFlashCard",
                table: "AppUserFlashCard");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "AppUserFlashCard",
                newName: "FlashCardID");

            migrationBuilder.RenameColumn(
                name: "FlashCardsFlashCardID",
                table: "AppUserFlashCard",
                newName: "AppUserID");

            migrationBuilder.RenameIndex(
                name: "IX_AppUserFlashCard_UsersId",
                table: "AppUserFlashCard",
                newName: "IX_AppUserFlashCard_FlashCardID");

            migrationBuilder.AddColumn<int>(
                name: "AppUserFlashCardID",
                table: "AppUserFlashCard",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppUserFlashCard",
                table: "AppUserFlashCard",
                column: "AppUserFlashCardID");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserFlashCard_AppUserID",
                table: "AppUserFlashCard",
                column: "AppUserID");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropIndex(
                name: "IX_AppUserFlashCard_AppUserID",
                table: "AppUserFlashCard");

            migrationBuilder.DropColumn(
                name: "AppUserFlashCardID",
                table: "AppUserFlashCard");

            migrationBuilder.RenameColumn(
                name: "FlashCardID",
                table: "AppUserFlashCard",
                newName: "UsersId");

            migrationBuilder.RenameColumn(
                name: "AppUserID",
                table: "AppUserFlashCard",
                newName: "FlashCardsFlashCardID");

            migrationBuilder.RenameIndex(
                name: "IX_AppUserFlashCard_FlashCardID",
                table: "AppUserFlashCard",
                newName: "IX_AppUserFlashCard_UsersId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppUserFlashCard",
                table: "AppUserFlashCard",
                columns: new[] { "FlashCardsFlashCardID", "UsersId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserFlashCard_AspNetUsers_UsersId",
                table: "AppUserFlashCard",
                column: "UsersId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserFlashCard_FlashCards_FlashCardsFlashCardID",
                table: "AppUserFlashCard",
                column: "FlashCardsFlashCardID",
                principalTable: "FlashCards",
                principalColumn: "FlashCardID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
