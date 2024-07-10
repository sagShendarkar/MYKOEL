using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyKoel_Domain.Migrations
{
    /// <inheritdoc />
    public partial class mainmenunewflags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Flag",
                table: "MainMenuGroups",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageIcon",
                table: "MainMenuGroups",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsIcon",
                table: "MainMenuGroups",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsImage",
                table: "MainMenuGroups",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPopup",
                table: "MainMenuGroups",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsRoute",
                table: "MainMenuGroups",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Flag",
                table: "MainMenuGroups");

            migrationBuilder.DropColumn(
                name: "ImageIcon",
                table: "MainMenuGroups");

            migrationBuilder.DropColumn(
                name: "IsIcon",
                table: "MainMenuGroups");

            migrationBuilder.DropColumn(
                name: "IsImage",
                table: "MainMenuGroups");

            migrationBuilder.DropColumn(
                name: "IsPopup",
                table: "MainMenuGroups");

            migrationBuilder.DropColumn(
                name: "IsRoute",
                table: "MainMenuGroups");
        }
    }
}
