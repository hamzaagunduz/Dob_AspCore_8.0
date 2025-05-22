using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig11_statictiks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserStatistics",
                columns: table => new
                {
                    UserStatisticId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserId = table.Column<int>(type: "int", nullable: false),
                    TotalScore = table.Column<int>(type: "int", nullable: false),
                    TotalTestsCompleted = table.Column<int>(type: "int", nullable: false),
                    PerfectTestsCompleted = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    League = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConsecutiveDays = table.Column<int>(type: "int", nullable: false),
                    ConsecutiveDaysTemp = table.Column<int>(type: "int", nullable: false),
                    LastTestDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStatistics", x => x.UserStatisticId);
                    table.ForeignKey(
                        name: "FK_UserStatistics_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserStatistics_AppUserId",
                table: "UserStatistics",
                column: "AppUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserStatistics");
        }
    }
}
