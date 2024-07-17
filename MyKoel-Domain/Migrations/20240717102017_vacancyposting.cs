using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyKoel_Domain.Migrations
{
    /// <inheritdoc />
    public partial class vacancyposting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VacancyInquiry",
                columns: table => new
                {
                    VACANCYINQUIRYID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VACANCYID = table.Column<int>(type: "int", nullable: false),
                    FIRSTNAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LASTNAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EMAIL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PHONENO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RESUMEPATH = table.Column<int>(type: "int", nullable: false),
                    COMMENT = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VacancyInquiry", x => x.VACANCYINQUIRYID);
                });

            migrationBuilder.CreateTable(
                name: "VacancyPosting",
                columns: table => new
                {
                    VACANCYID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GRADE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JOBTITLE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VACANCYCOUNT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DEPARTMENT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LOCATION = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JOBTYPE = table.Column<int>(type: "int", nullable: false),
                    SALARYRANGE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JOBDESC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    REQUIRMENTS = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    POSTEDDATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CLOSINGDATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CONTACTINFO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    STATUS = table.Column<int>(type: "int", nullable: false),
                    ISACTIVE = table.Column<bool>(type: "bit", nullable: false),
                    CREATEDBY = table.Column<int>(type: "int", nullable: false),
                    CREATEDDATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UPDATEDBY = table.Column<int>(type: "int", nullable: false),
                    UPDATEDDATE = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VacancyPosting", x => x.VACANCYID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VacancyInquiry");

            migrationBuilder.DropTable(
                name: "VacancyPosting");
        }
    }
}
