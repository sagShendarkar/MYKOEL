﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyKoel_Domain.Data;

#nullable disable

namespace MyKoel_Domain.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240724115211_BirthdayMessage")]
    partial class BirthdayMessage
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("API.Entities.AppRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("API.Entities.AppUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("AppName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CostCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DOB")
                        .HasColumnType("datetime2");

                    b.Property<string>("Department")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EMPID")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("EmpName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Grade")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastActive")
                        .HasColumnType("datetime2");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("MANAGERID")
                        .HasColumnType("int");

                    b.Property<string>("ManagerEmailID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ManagerTicketNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("ProfileImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SBUNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TicketNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("API.Entities.AppUserRole", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("MyKoel_Domain.Models.Master.MainMenuGroup", b =>
                {
                    b.Property<int>("MainMenuGroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MainMenuGroupId"));

                    b.Property<string>("Flag")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Icon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageIcon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsChild")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsIcon")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsImage")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsPopup")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsRoute")
                        .HasColumnType("bit");

                    b.Property<string>("MenuGroupName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Route")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Sequence")
                        .HasColumnType("int");

                    b.HasKey("MainMenuGroupId");

                    b.ToTable("MainMenuGroups");
                });

            modelBuilder.Entity("MyKoel_Domain.Models.Master.MenuGroup", b =>
                {
                    b.Property<int>("MenuGroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MenuGroupId"));

                    b.Property<string>("GroupName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Icon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsChild")
                        .HasColumnType("bit");

                    b.Property<int?>("MainMenuGroupId")
                        .HasColumnType("int");

                    b.Property<string>("Route")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Sequence")
                        .HasColumnType("int");

                    b.HasKey("MenuGroupId");

                    b.HasIndex("MainMenuGroupId");

                    b.ToTable("MenuGroups");
                });

            modelBuilder.Entity("MyKoel_Domain.Models.Master.MoodToday", b =>
                {
                    b.Property<int>("MoodId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MoodId"));

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rating")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ReportedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("MoodId");

                    b.HasIndex("UserId");

                    b.ToTable("MoodToday");
                });

            modelBuilder.Entity("MyKoel_Domain.Models.Master.UserAccessMapping", b =>
                {
                    b.Property<int>("AccessMappingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AccessMappingId"));

                    b.Property<int?>("MainMenuGroupId")
                        .HasColumnType("int");

                    b.Property<int?>("MenuGroupId")
                        .HasColumnType("int");

                    b.Property<int?>("MenuId")
                        .HasColumnType("int");

                    b.Property<int?>("MenusId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("AccessMappingId");

                    b.HasIndex("MenuId");

                    b.HasIndex("UserId");

                    b.ToTable("UserMenuMap");
                });

            modelBuilder.Entity("MyKoel_Domain.Models.Masters.Attachments", b =>
                {
                    b.Property<int>("ATTACHMENTID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ATTACHMENTID"));

                    b.Property<int>("CREATEDBY")
                        .HasColumnType("int");

                    b.Property<DateTime>("CREATEDDATE")
                        .HasColumnType("datetime2");

                    b.Property<string>("FILENAME")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FILETYPE")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("IMAGEFLAG")
                        .HasColumnType("int");

                    b.Property<bool>("ISACTIVE")
                        .HasColumnType("bit");

                    b.Property<bool>("ISPOPUP")
                        .HasColumnType("bit");

                    b.Property<bool>("ISREDIRECTED")
                        .HasColumnType("bit");

                    b.Property<string>("PATH")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SECTIONID")
                        .HasColumnType("int");

                    b.Property<string>("TITLE")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UPDATEDBY")
                        .HasColumnType("int");

                    b.Property<DateTime>("UPDATEDDATE")
                        .HasColumnType("datetime2");

                    b.HasKey("ATTACHMENTID");

                    b.HasIndex("SECTIONID");

                    b.ToTable("Attachments");
                });

            modelBuilder.Entity("MyKoel_Domain.Models.Masters.BirthdayMessage", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("CREATEDBY")
                        .HasColumnType("int");

                    b.Property<DateTime>("CREATEDDATE")
                        .HasColumnType("datetime2");

                    b.Property<int>("DAY")
                        .HasColumnType("int");

                    b.Property<bool>("ISACTIVE")
                        .HasColumnType("bit");

                    b.Property<string>("MESSAGE")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UPDATEDBY")
                        .HasColumnType("int");

                    b.Property<DateTime>("UPDATEDDATE")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.ToTable("BirthdayMessages");
                });

            modelBuilder.Entity("MyKoel_Domain.Models.Masters.BreakFast", b =>
                {
                    b.Property<int>("BREAKFASTID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BREAKFASTID"));

                    b.Property<string>("BREAKFASTNAME")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ISACTIVE")
                        .HasColumnType("bit");

                    b.HasKey("BREAKFASTID");

                    b.ToTable("BreakFasts");
                });

            modelBuilder.Entity("MyKoel_Domain.Models.Masters.CanteenMenus", b =>
                {
                    b.Property<int>("CANTEENMENUSID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CANTEENMENUSID"));

                    b.Property<int?>("BREAKFASTID")
                        .HasColumnType("int");

                    b.Property<DateTime>("DATE")
                        .HasColumnType("datetime2");

                    b.Property<int?>("LUNCHID")
                        .HasColumnType("int");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CANTEENMENUSID");

                    b.ToTable("CanteenMenus");
                });

            modelBuilder.Entity("MyKoel_Domain.Models.Masters.HolidayCalendar", b =>
                {
                    b.Property<int>("HOLIDAYCALENDERID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HOLIDAYCALENDERID"));

                    b.Property<string>("BATCHID")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DATE")
                        .HasColumnType("datetime2");

                    b.Property<string>("DAY")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HOLIDAY")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ISACTIVE")
                        .HasColumnType("bit");

                    b.Property<string>("LOCATION")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("REMARKS")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("YEAR")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("HOLIDAYCALENDERID");

                    b.ToTable("HolidayCalendars");
                });

            modelBuilder.Entity("MyKoel_Domain.Models.Masters.LunchMaster", b =>
                {
                    b.Property<int>("LUNCHID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LUNCHID"));

                    b.Property<bool>("ISACTIVE")
                        .HasColumnType("bit");

                    b.Property<string>("LUNCHNAME")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LUNCHID");

                    b.ToTable("LunchMaster");
                });

            modelBuilder.Entity("MyKoel_Domain.Models.Masters.MenuMaster", b =>
                {
                    b.Property<int>("MenusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MenusId"));

                    b.Property<string>("Flag")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Icon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageIcon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsChild")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsIcon")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsImage")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsPopup")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsRoute")
                        .HasColumnType("bit");

                    b.Property<int?>("Level")
                        .HasColumnType("int");

                    b.Property<string>("MenuName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.Property<string>("Route")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Sequence")
                        .HasColumnType("int");

                    b.HasKey("MenusId");

                    b.ToTable("MenuMaster");
                });

            modelBuilder.Entity("MyKoel_Domain.Models.Masters.Menus", b =>
                {
                    b.Property<int>("MenuId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MenuId"));

                    b.Property<string>("Icon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int?>("MenuGroupId")
                        .HasColumnType("int");

                    b.Property<string>("MenuName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Route")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Sequence")
                        .HasColumnType("int");

                    b.HasKey("MenuId");

                    b.HasIndex("MenuGroupId");

                    b.ToTable("Menus");
                });

            modelBuilder.Entity("MyKoel_Domain.Models.Masters.QuickLinks", b =>
                {
                    b.Property<int>("QuickLinkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("QuickLinkId"));

                    b.Property<int>("CREATEDBY")
                        .HasColumnType("int");

                    b.Property<DateTime>("CREATEDDATE")
                        .HasColumnType("datetime2");

                    b.Property<string>("Icon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Sequence")
                        .HasColumnType("int");

                    b.Property<int>("UPDATEDBY")
                        .HasColumnType("int");

                    b.Property<DateTime>("UPDATEDDATE")
                        .HasColumnType("datetime2");

                    b.Property<string>("URL")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("QuickLinkId");

                    b.ToTable("QuickLinks");
                });

            modelBuilder.Entity("MyKoel_Domain.Models.Masters.SectionTransaction", b =>
                {
                    b.Property<int>("SECTIONID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SECTIONID"));

                    b.Property<string>("CATEGORY")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CREATEDBY")
                        .HasColumnType("int");

                    b.Property<DateTime>("CREATEDDATE")
                        .HasColumnType("datetime2");

                    b.Property<string>("DESCRIPTION")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ENDDATE")
                        .HasColumnType("datetime2");

                    b.Property<string>("FLAG")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ISACTIVE")
                        .HasColumnType("bit");

                    b.Property<bool>("ISIMAGE")
                        .HasColumnType("bit");

                    b.Property<bool>("IsHtml")
                        .HasColumnType("bit");

                    b.Property<string>("SEQUENCE")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("STARTDATE")
                        .HasColumnType("datetime2");

                    b.Property<string>("TITLE")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UPDATEDBY")
                        .HasColumnType("int");

                    b.Property<DateTime>("UPDATEDDATE")
                        .HasColumnType("datetime2");

                    b.HasKey("SECTIONID");

                    b.ToTable("SectionTransactions");
                });

            modelBuilder.Entity("MyKoel_Domain.Models.Masters.VacancyInquiry", b =>
                {
                    b.Property<int>("VACANCYINQUIRYID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VACANCYINQUIRYID"));

                    b.Property<string>("COMMENT")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EMAIL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FIRSTNAME")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LASTNAME")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PHONENO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RESUMEPATH")
                        .HasColumnType("int");

                    b.Property<int>("VACANCYID")
                        .HasColumnType("int");

                    b.HasKey("VACANCYINQUIRYID");

                    b.ToTable("VacancyInquiry");
                });

            modelBuilder.Entity("MyKoel_Domain.Models.Masters.VacancyPosting", b =>
                {
                    b.Property<int>("VACANCYID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VACANCYID"));

                    b.Property<DateTime>("CLOSINGDATE")
                        .HasColumnType("datetime2");

                    b.Property<string>("CONTACTINFO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CREATEDBY")
                        .HasColumnType("int");

                    b.Property<DateTime>("CREATEDDATE")
                        .HasColumnType("datetime2");

                    b.Property<string>("DEPARTMENT")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FILETYPE")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GRADE")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ISACTIVE")
                        .HasColumnType("bit");

                    b.Property<string>("JOBDESC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("JOBTITLE")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("JOBTYPE")
                        .HasColumnType("int");

                    b.Property<string>("LOCATION")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("POSTEDDATE")
                        .HasColumnType("datetime2");

                    b.Property<string>("REQUIRMENTS")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SALARYRANGE")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("STATUS")
                        .HasColumnType("int");

                    b.Property<int>("UPDATEDBY")
                        .HasColumnType("int");

                    b.Property<DateTime>("UPDATEDDATE")
                        .HasColumnType("datetime2");

                    b.Property<string>("VACANCYCOUNT")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("VACANCYID");

                    b.ToTable("VacancyPosting");
                });

            modelBuilder.Entity("MyKoel_Domain.Models.Masters.ValuesMaster", b =>
                {
                    b.Property<int>("VALUEID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VALUEID"));

                    b.Property<int>("CREATEDBY")
                        .HasColumnType("int");

                    b.Property<DateTime>("CREATEDDATE")
                        .HasColumnType("datetime2");

                    b.Property<string>("DESCRIPTION")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ISACTIVE")
                        .HasColumnType("bit");

                    b.Property<int>("TYPE")
                        .HasColumnType("int");

                    b.Property<int>("UPDATEDBY")
                        .HasColumnType("int");

                    b.Property<DateTime>("UPDATEDDATE")
                        .HasColumnType("datetime2");

                    b.HasKey("VALUEID");

                    b.ToTable("ValuesMaster");
                });

            modelBuilder.Entity("MyKoel_Domain.Models.Masters.Wallpaper", b =>
                {
                    b.Property<int>("WallpaperId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WallpaperId"));

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("WallpaperName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WallpaperPath")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("WallpaperId");

                    b.ToTable("wallpaper");
                });

            modelBuilder.Entity("API.Entities.AppUserRole", b =>
                {
                    b.HasOne("API.Entities.AppRole", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("API.Entities.AppUser", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("API.Entities.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("API.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("API.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("API.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MyKoel_Domain.Models.Master.MenuGroup", b =>
                {
                    b.HasOne("MyKoel_Domain.Models.Master.MainMenuGroup", "MainMenuGroup")
                        .WithMany("MenuGroups")
                        .HasForeignKey("MainMenuGroupId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("MainMenuGroup");
                });

            modelBuilder.Entity("MyKoel_Domain.Models.Master.MoodToday", b =>
                {
                    b.HasOne("API.Entities.AppUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("MyKoel_Domain.Models.Master.UserAccessMapping", b =>
                {
                    b.HasOne("MyKoel_Domain.Models.Masters.Menus", "Menu")
                        .WithMany("userMenuMaps")
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("API.Entities.AppUser", "User")
                        .WithMany("userMenuMaps")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Menu");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MyKoel_Domain.Models.Masters.Attachments", b =>
                {
                    b.HasOne("MyKoel_Domain.Models.Masters.SectionTransaction", "SectionTransaction")
                        .WithMany("Attachments")
                        .HasForeignKey("SECTIONID")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("SectionTransaction");
                });

            modelBuilder.Entity("MyKoel_Domain.Models.Masters.Menus", b =>
                {
                    b.HasOne("MyKoel_Domain.Models.Master.MenuGroup", "MenuGroup")
                        .WithMany("Menus")
                        .HasForeignKey("MenuGroupId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("MenuGroup");
                });

            modelBuilder.Entity("API.Entities.AppRole", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("API.Entities.AppUser", b =>
                {
                    b.Navigation("UserRoles");

                    b.Navigation("userMenuMaps");
                });

            modelBuilder.Entity("MyKoel_Domain.Models.Master.MainMenuGroup", b =>
                {
                    b.Navigation("MenuGroups");
                });

            modelBuilder.Entity("MyKoel_Domain.Models.Master.MenuGroup", b =>
                {
                    b.Navigation("Menus");
                });

            modelBuilder.Entity("MyKoel_Domain.Models.Masters.Menus", b =>
                {
                    b.Navigation("userMenuMaps");
                });

            modelBuilder.Entity("MyKoel_Domain.Models.Masters.SectionTransaction", b =>
                {
                    b.Navigation("Attachments");
                });
#pragma warning restore 612, 618
        }
    }
}
