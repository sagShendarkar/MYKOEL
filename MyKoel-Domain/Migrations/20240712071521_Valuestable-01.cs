using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyKoel_Domain.Migrations
{
    /// <inheritdoc />
    public partial class Valuestable01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LunchName",
                table: "LunchMaster",
                newName: "LUNCHNAME");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "LunchMaster",
                newName: "ISACTIVE");

            migrationBuilder.RenameColumn(
                name: "LunchId",
                table: "LunchMaster",
                newName: "LUNCHID");

            migrationBuilder.RenameColumn(
                name: "LunchId",
                table: "CanteenMenus",
                newName: "LUNCHID");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "CanteenMenus",
                newName: "DATE");

            migrationBuilder.RenameColumn(
                name: "BreakFastId",
                table: "CanteenMenus",
                newName: "BREAKFASTID");

            migrationBuilder.RenameColumn(
                name: "CanteenMenusId",
                table: "CanteenMenus",
                newName: "CANTEENMENUSID");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "BreakFasts",
                newName: "ISACTIVE");

            migrationBuilder.RenameColumn(
                name: "BreakFastName",
                table: "BreakFasts",
                newName: "BREAKFASTNAME");

            migrationBuilder.RenameColumn(
                name: "BreakFastId",
                table: "BreakFasts",
                newName: "BREAKFASTID");

            migrationBuilder.CreateTable(
                name: "ValuesMaster",
                columns: table => new
                {
                    VALUEID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DESCRIPTION = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TYPE = table.Column<int>(type: "int", nullable: false),
                    ISACTIVE = table.Column<bool>(type: "bit", nullable: false),
                    CREATEDBY = table.Column<int>(type: "int", nullable: false),
                    CREATEDDATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UPDATEDBY = table.Column<int>(type: "int", nullable: false),
                    UPDATEDDATE = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValuesMaster", x => x.VALUEID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ValuesMaster");

            migrationBuilder.RenameColumn(
                name: "LUNCHNAME",
                table: "LunchMaster",
                newName: "LunchName");

            migrationBuilder.RenameColumn(
                name: "ISACTIVE",
                table: "LunchMaster",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "LUNCHID",
                table: "LunchMaster",
                newName: "LunchId");

            migrationBuilder.RenameColumn(
                name: "LUNCHID",
                table: "CanteenMenus",
                newName: "LunchId");

            migrationBuilder.RenameColumn(
                name: "DATE",
                table: "CanteenMenus",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "BREAKFASTID",
                table: "CanteenMenus",
                newName: "BreakFastId");

            migrationBuilder.RenameColumn(
                name: "CANTEENMENUSID",
                table: "CanteenMenus",
                newName: "CanteenMenusId");

            migrationBuilder.RenameColumn(
                name: "ISACTIVE",
                table: "BreakFasts",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "BREAKFASTNAME",
                table: "BreakFasts",
                newName: "BreakFastName");

            migrationBuilder.RenameColumn(
                name: "BREAKFASTID",
                table: "BreakFasts",
                newName: "BreakFastId");
        }
    }
}
