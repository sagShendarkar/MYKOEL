using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyKoel_Domain.Migrations
{
    /// <inheritdoc />
    public partial class IntialDb10724 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                table: "QuickLinks",
                newName: "UPDATEDDATE");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                table: "QuickLinks",
                newName: "UPDATEDBY");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "QuickLinks",
                newName: "CREATEDDATE");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "QuickLinks",
                newName: "CREATEDBY");

            migrationBuilder.CreateTable(
                name: "SectionTransactions",
                columns: table => new
                {
                    SECTIONID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TITLE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DESCRIPTION = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ISIMAGE = table.Column<bool>(type: "bit", nullable: false),
                    STARTDATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ENDDATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FLAG = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SEQUENCE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ISACTIVE = table.Column<bool>(type: "bit", nullable: false),
                    CREATEDBY = table.Column<int>(type: "int", nullable: false),
                    CREATEDDATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UPDATEDBY = table.Column<int>(type: "int", nullable: false),
                    UPDATEDDATE = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectionTransactions", x => x.SECTIONID);
                });

            migrationBuilder.CreateTable(
                name: "Attachments",
                columns: table => new
                {
                    ATTACHMENTID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SECTIONID = table.Column<int>(type: "int", nullable: false),
                    PATH = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FILENAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FILETYPE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TITLE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ISPOPUP = table.Column<bool>(type: "bit", nullable: false),
                    ISREDIRECTED = table.Column<bool>(type: "bit", nullable: false),
                    ISACTIVE = table.Column<bool>(type: "bit", nullable: false),
                    CREATEDBY = table.Column<int>(type: "int", nullable: false),
                    CREATEDDATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UPDATEDBY = table.Column<int>(type: "int", nullable: false),
                    UPDATEDDATE = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachments", x => x.ATTACHMENTID);
                    table.ForeignKey(
                        name: "FK_Attachments_SectionTransactions_SECTIONID",
                        column: x => x.SECTIONID,
                        principalTable: "SectionTransactions",
                        principalColumn: "SECTIONID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_SECTIONID",
                table: "Attachments",
                column: "SECTIONID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attachments");

            migrationBuilder.DropTable(
                name: "SectionTransactions");

            migrationBuilder.RenameColumn(
                name: "UPDATEDDATE",
                table: "QuickLinks",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "UPDATEDBY",
                table: "QuickLinks",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "CREATEDDATE",
                table: "QuickLinks",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "CREATEDBY",
                table: "QuickLinks",
                newName: "CreatedBy");
        }
    }
}
