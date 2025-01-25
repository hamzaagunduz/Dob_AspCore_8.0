using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class flashcard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserFlashCard_FlashCard_FlashCardsFlashCardID",
                table: "AppUserFlashCard");

            migrationBuilder.DropForeignKey(
                name: "FK_FlashCard_Questions_QuestionID",
                table: "FlashCard");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FlashCard",
                table: "FlashCard");

            migrationBuilder.DropColumn(
                name: "Selected",
                table: "Exams");

            migrationBuilder.RenameTable(
                name: "FlashCard",
                newName: "FlashCards");

            migrationBuilder.RenameIndex(
                name: "IX_FlashCard_QuestionID",
                table: "FlashCards",
                newName: "IX_FlashCards_QuestionID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FlashCards",
                table: "FlashCards",
                column: "FlashCardID");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserFlashCard_FlashCards_FlashCardsFlashCardID",
                table: "AppUserFlashCard",
                column: "FlashCardsFlashCardID",
                principalTable: "FlashCards",
                principalColumn: "FlashCardID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FlashCards_Questions_QuestionID",
                table: "FlashCards",
                column: "QuestionID",
                principalTable: "Questions",
                principalColumn: "QuestionID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserFlashCard_FlashCards_FlashCardsFlashCardID",
                table: "AppUserFlashCard");

            migrationBuilder.DropForeignKey(
                name: "FK_FlashCards_Questions_QuestionID",
                table: "FlashCards");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FlashCards",
                table: "FlashCards");

            migrationBuilder.RenameTable(
                name: "FlashCards",
                newName: "FlashCard");

            migrationBuilder.RenameIndex(
                name: "IX_FlashCards_QuestionID",
                table: "FlashCard",
                newName: "IX_FlashCard_QuestionID");

            migrationBuilder.AddColumn<bool>(
                name: "Selected",
                table: "Exams",
                type: "bit",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FlashCard",
                table: "FlashCard",
                column: "FlashCardID");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserFlashCard_FlashCard_FlashCardsFlashCardID",
                table: "AppUserFlashCard",
                column: "FlashCardsFlashCardID",
                principalTable: "FlashCard",
                principalColumn: "FlashCardID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FlashCard_Questions_QuestionID",
                table: "FlashCard",
                column: "QuestionID",
                principalTable: "Questions",
                principalColumn: "QuestionID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
