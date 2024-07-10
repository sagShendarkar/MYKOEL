using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyKoel_Domain.Migrations
{
    /// <inheritdoc />
    public partial class UserNewColumns02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "MenuId",
                table: "UserMenuMap",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "MainMenuGroupId",
                table: "UserMenuMap",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MenuGroupId",
                table: "UserMenuMap",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainMenuGroupId",
                table: "UserMenuMap");

            migrationBuilder.DropColumn(
                name: "MenuGroupId",
                table: "UserMenuMap");

            migrationBuilder.AlterColumn<int>(
                name: "MenuId",
                table: "UserMenuMap",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
