using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyKoel_Domain.Migrations
{
    /// <inheritdoc />
    public partial class moodtoday1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__moodtoday_AspNetUsers_UserId",
                table: "_moodtoday");

            migrationBuilder.DropPrimaryKey(
                name: "PK__moodtoday",
                table: "_moodtoday");

            migrationBuilder.RenameTable(
                name: "_moodtoday",
                newName: "MoodToday");

            migrationBuilder.RenameIndex(
                name: "IX__moodtoday_UserId",
                table: "MoodToday",
                newName: "IX_MoodToday_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MoodToday",
                table: "MoodToday",
                column: "MoodId");

            migrationBuilder.AddForeignKey(
                name: "FK_MoodToday_AspNetUsers_UserId",
                table: "MoodToday",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MoodToday_AspNetUsers_UserId",
                table: "MoodToday");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MoodToday",
                table: "MoodToday");

            migrationBuilder.RenameTable(
                name: "MoodToday",
                newName: "_moodtoday");

            migrationBuilder.RenameIndex(
                name: "IX_MoodToday_UserId",
                table: "_moodtoday",
                newName: "IX__moodtoday_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK__moodtoday",
                table: "_moodtoday",
                column: "MoodId");

            migrationBuilder.AddForeignKey(
                name: "FK__moodtoday_AspNetUsers_UserId",
                table: "_moodtoday",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
