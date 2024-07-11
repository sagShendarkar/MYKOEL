using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyKoel_Domain.Migrations
{
    /// <inheritdoc />
    public partial class canteentables01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BreakFast",
                table: "CanteenMenus",
                newName: "BreakFastId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BreakFastId",
                table: "CanteenMenus",
                newName: "BreakFast");
        }
    }
}
