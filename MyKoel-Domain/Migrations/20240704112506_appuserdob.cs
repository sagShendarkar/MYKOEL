using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyKoel_Domain.Migrations
{
    /// <inheritdoc />
    public partial class appuserdob : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Wallpaper",
                table: "Wallpaper");

            migrationBuilder.RenameTable(
                name: "Wallpaper",
                newName: "wallpaper");

            migrationBuilder.AddColumn<DateTime>(
                name: "DOB",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_wallpaper",
                table: "wallpaper",
                column: "WallpaperId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_wallpaper",
                table: "wallpaper");

            migrationBuilder.DropColumn(
                name: "DOB",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "wallpaper",
                newName: "Wallpaper");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wallpaper",
                table: "Wallpaper",
                column: "WallpaperId");
        }
    }
}
