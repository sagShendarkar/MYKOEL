using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyKoel_Domain.Migrations
{
    /// <inheritdoc />
    public partial class canteentables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BreakFasts",
                columns: table => new
                {
                    BreakFastId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BreakFastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BreakFasts", x => x.BreakFastId);
                });

            migrationBuilder.CreateTable(
                name: "CanteenMenus",
                columns: table => new
                {
                    CanteenMenusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LunchId = table.Column<int>(type: "int", nullable: true),
                    BreakFast = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CanteenMenus", x => x.CanteenMenusId);
                });

            migrationBuilder.CreateTable(
                name: "LunchMaster",
                columns: table => new
                {
                    LunchId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LunchName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LunchMaster", x => x.LunchId);
                });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BreakFasts");

            migrationBuilder.DropTable(
                name: "CanteenMenus");

            migrationBuilder.DropTable(
                name: "LunchMaster");


        }
    }
}
