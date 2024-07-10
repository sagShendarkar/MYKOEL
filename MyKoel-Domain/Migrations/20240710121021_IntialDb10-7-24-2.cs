using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyKoel_Domain.Migrations
{
    /// <inheritdoc />
    public partial class IntialDb107242 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Category",
                table: "SectionTransactions",
                newName: "CATEGORY");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CATEGORY",
                table: "SectionTransactions",
                newName: "Category");
        }
    }
}
