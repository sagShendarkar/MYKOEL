using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyKoel_Domain.Migrations
{
    /// <inheritdoc />
    public partial class VacancyNewColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FILETYPE",
                table: "VacancyPosting",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FILETYPE",
                table: "VacancyPosting");
        }
    }
}
