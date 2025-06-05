using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class testgroupmig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tests_Topics_TopicID",
                table: "Tests");

            migrationBuilder.DropIndex(
                name: "IX_Tests_TopicID",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "TopicID",
                table: "Tests");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TopicID",
                table: "Tests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tests_TopicID",
                table: "Tests",
                column: "TopicID");

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_Topics_TopicID",
                table: "Tests",
                column: "TopicID",
                principalTable: "Topics",
                principalColumn: "TopicID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
