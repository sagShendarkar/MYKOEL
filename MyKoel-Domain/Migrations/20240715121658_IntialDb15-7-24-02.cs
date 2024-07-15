using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyKoel_Domain.Migrations
{
    /// <inheritdoc />
    public partial class IntialDb1572402 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HolidayCalendars",
                columns: table => new
                {
                    HOLIDAYCALENDERID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HOLIDAY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DAY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    REMARKS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LOCATION = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YEAR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ISACTIVE = table.Column<bool>(type: "bit", nullable: false),
                    BATCHID = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HolidayCalendars", x => x.HOLIDAYCALENDERID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HolidayCalendars");
        }
    }
}
