using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class testgroupmig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description2",
                table: "Tests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TestGroupID",
                table: "Tests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TestGroups",
                columns: table => new
                {
                    TestGroupID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    test = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TopicID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestGroups", x => x.TestGroupID);
                    table.ForeignKey(
                        name: "FK_TestGroups_Topics_TopicID",
                        column: x => x.TopicID,
                        principalTable: "Topics",
                        principalColumn: "TopicID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tests_TestGroupID",
                table: "Tests",
                column: "TestGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_TestGroups_TopicID",
                table: "TestGroups",
                column: "TopicID");

            migrationBuilder.AddForeignKey(
                name: "FK_Tests_TestGroups_TestGroupID",
                table: "Tests",
                column: "TestGroupID",
                principalTable: "TestGroups",
                principalColumn: "TestGroupID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tests_TestGroups_TestGroupID",
                table: "Tests");

            migrationBuilder.DropTable(
                name: "TestGroups");

            migrationBuilder.DropIndex(
                name: "IX_Tests_TestGroupID",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "Description2",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "TestGroupID",
                table: "Tests");
        }
    }
}
