using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StreetAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyType = table.Column<int>(type: "int", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeadquarterId = table.Column<int>(type: "int", nullable: false),
                    Industry = table.Column<int>(type: "int", nullable: false),
                    LogoImageUri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParentCompanyId = table.Column<int>(type: "int", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TradeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WebsiteUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Company_Address_HeadquarterId",
                        column: x => x.HeadquarterId,
                        principalTable: "Address",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Company_Company_ParentCompanyId",
                        column: x => x.ParentCompanyId,
                        principalTable: "Company",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Console",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeveloperId = table.Column<int>(type: "int", nullable: false),
                    ImageUri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Console", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Console_Company_DeveloperId",
                        column: x => x.DeveloperId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VideoGame",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalTimePlayed = table.Column<TimeSpan>(type: "time", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeveloperId = table.Column<int>(type: "int", nullable: false),
                    ImageUri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoGame", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VideoGame_Company_DeveloperId",
                        column: x => x.DeveloperId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConsoleVideoGame",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConsoleId = table.Column<int>(type: "int", nullable: false),
                    VideoGameId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsoleVideoGame", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConsoleVideoGame_Console_ConsoleId",
                        column: x => x.ConsoleId,
                        principalTable: "Console",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ConsoleVideoGame_VideoGame_VideoGameId",
                        column: x => x.VideoGameId,
                        principalTable: "VideoGame",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConsoleId = table.Column<int>(type: "int", nullable: true),
                    Grade = table.Column<int>(type: "int", nullable: false),
                    ReviewDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReviewText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VideoGameId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Review_Console_ConsoleId",
                        column: x => x.ConsoleId,
                        principalTable: "Console",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Review_VideoGame_VideoGameId",
                        column: x => x.VideoGameId,
                        principalTable: "VideoGame",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Trophy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IconUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrophyValue = table.Column<int>(type: "int", nullable: false),
                    VideoGameId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trophy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trophy_VideoGame_VideoGameId",
                        column: x => x.VideoGameId,
                        principalTable: "VideoGame",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VideoGameGenre",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenreId = table.Column<int>(type: "int", nullable: false),
                    VideoGameId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoGameGenre", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VideoGameGenre_Genre_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genre",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_VideoGameGenre_VideoGame_VideoGameId",
                        column: x => x.VideoGameId,
                        principalTable: "VideoGame",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "Id", "City", "Country", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "PostalCode", "State", "StreetAddress", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, "San Mateo", "USA", new DateTime(2024, 8, 9, 17, 2, 56, 531, DateTimeKind.Local).AddTicks(7015), "System", null, null, "94404", "California", "2207 Bridgepointe Pkwy", null, null },
                    { 2, "San Mateo", "United States", new DateTime(2024, 8, 9, 17, 2, 56, 531, DateTimeKind.Local).AddTicks(7065), "System", null, null, "94404", "California", "2207 Bridgepointe Pkwy", null, null },
                    { 3, "Amsterdam", "The Netherlands", new DateTime(2024, 8, 9, 17, 2, 56, 531, DateTimeKind.Local).AddTicks(7068), "System", null, null, "1012 RL", "", "Nieuwezijds Voorburgwal 225", null, null },
                    { 4, "Bellevue", "United States", new DateTime(2024, 8, 9, 17, 2, 56, 531, DateTimeKind.Local).AddTicks(7108), "System", null, null, "98004", "Washington", "500 108th Avenue North East Suite 2600", null, null }
                });

            migrationBuilder.InsertData(
                table: "Genre",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Description", "Name", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, null, null, null, null, null, "Action", null, null },
                    { 2, null, null, null, null, null, "Adventure", null, null },
                    { 3, null, null, null, null, null, "Puzzle", null, null },
                    { 4, null, null, null, null, null, "Casual", null, null },
                    { 5, null, null, null, null, null, "Role Playing Games", null, null },
                    { 6, null, null, null, null, null, "Arcade", null, null },
                    { 7, null, null, null, null, null, "Shooter", null, null },
                    { 8, null, null, null, null, null, "Simulation", null, null },
                    { 9, null, null, null, null, null, "Strategy", null, null },
                    { 10, null, null, null, null, null, "Horror", null, null },
                    { 11, null, null, null, null, null, "Driving/Racing", null, null },
                    { 12, null, null, null, null, null, "Unique", null, null },
                    { 13, null, null, null, null, null, "Sport", null, null },
                    { 14, null, null, null, null, null, "Family", null, null },
                    { 15, null, null, null, null, null, "Fighting", null, null },
                    { 16, null, null, null, null, null, "Party", null, null },
                    { 17, null, null, null, null, null, "Simulator", null, null },
                    { 18, null, null, null, null, null, "Music/Rythm", null, null },
                    { 19, null, null, null, null, null, "Adult", null, null },
                    { 20, null, null, null, null, null, "Brain Training", null, null },
                    { 21, null, null, null, null, null, "Educational", null, null },
                    { 22, null, null, null, null, null, "Fitness", null, null },
                    { 23, null, null, null, null, null, "Quiz", null, null }
                });

            migrationBuilder.InsertData(
                table: "Company",
                columns: new[] { "Id", "CompanyType", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "EmailAddress", "HeadquarterId", "Industry", "LogoImageUri", "Name", "ParentCompanyId", "PhoneNumber", "TradeName", "UpdatedAt", "UpdatedBy", "WebsiteUrl" },
                values: new object[,]
                {
                    { 1, 0, new DateTime(2024, 8, 9, 17, 2, 56, 531, DateTimeKind.Local).AddTicks(7383), "System", null, null, "", 1, 0, null, "Sony Interactive Entertainment", null, "", "Sony Interactive Entertainment", null, null, "https://sonyinteractive.com/en/" },
                    { 2, 2, new DateTime(2024, 8, 9, 17, 2, 56, 531, DateTimeKind.Local).AddTicks(7388), "System", null, null, "", 2, 0, null, "PlayStation Studios", 2, "", "PlayStation Studios", null, null, "https://www.playstation.com/en-us/corporate/playstation-studios/" },
                    { 3, 0, new DateTime(2024, 8, 9, 17, 2, 56, 531, DateTimeKind.Local).AddTicks(7391), "System", null, null, "", 3, 0, null, "Guerrilla", 2, "", "Guerrilla Games", null, null, "https://www.guerrilla-games.com/" },
                    { 4, 0, new DateTime(2024, 8, 9, 17, 2, 56, 531, DateTimeKind.Local).AddTicks(7394), "System", null, null, "", 4, 0, null, "Sucker Punch", 2, "", "Sucker Punch Productions", null, null, "https://www.suckerpunch.com/" }
                });

            migrationBuilder.InsertData(
                table: "Console",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "DeveloperId", "ImageUri", "Name", "Price", "PurchaseDate", "ReleaseDate", "UpdatedAt", "UpdatedBy", "Url" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 8, 9, 17, 2, 56, 531, DateTimeKind.Local).AddTicks(7629), "System", null, null, 1, null, "PlayStation 5", 9988.0m, new DateTime(2022, 1, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 11, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "https://www.playstation.com/sv-se/ps5/" },
                    { 2, new DateTime(2024, 8, 9, 17, 2, 56, 531, DateTimeKind.Local).AddTicks(7638), "System", null, null, 1, null, "PlayStation VR2", 7869.0m, new DateTime(2023, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "https://www.playstation.com/sv-se/ps-vr2/" }
                });

            migrationBuilder.InsertData(
                table: "Review",
                columns: new[] { "Id", "ConsoleId", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Grade", "ReviewDate", "ReviewText", "UpdatedAt", "UpdatedBy", "VideoGameId" },
                values: new object[] { 1, 1, new DateTime(2024, 8, 9, 17, 2, 56, 531, DateTimeKind.Local).AddTicks(8146), "System", null, null, 100, new DateTime(2024, 8, 9, 17, 2, 56, 531, DateTimeKind.Local).AddTicks(8143), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", null, null, null });

            migrationBuilder.InsertData(
                table: "VideoGame",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "DeveloperId", "ImageUri", "Name", "Price", "PurchaseDate", "ReleaseDate", "Title", "TotalTimePlayed", "UpdatedAt", "UpdatedBy", "Url" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 8, 9, 17, 2, 56, 531, DateTimeKind.Local).AddTicks(8404), "System", null, null, 3, null, "Horizon Zero Dawn - Complete Edition", 229.0m, new DateTime(2023, 4, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2019, 6, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Horizon Zero Dawn - Complete Edition", new TimeSpan(0, 0, 0, 0, 0), null, null, "https://store.playstation.com/sv-se/product/EP9000-CUSA10211_00-HRZCE00000000000" },
                    { 2, new DateTime(2024, 8, 9, 17, 2, 56, 531, DateTimeKind.Local).AddTicks(8412), "System", null, null, 3, null, "Horizon Forbidden West - Complete Edition", 799.0m, new DateTime(2023, 11, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Horizon Forbidden West - Complete Edition", new TimeSpan(0, 0, 0, 0, 0), null, null, "https://www.playstation.com/sv-se/games/horizon-forbidden-west/" },
                    { 3, new DateTime(2024, 8, 9, 17, 2, 56, 531, DateTimeKind.Local).AddTicks(8420), "System", null, null, 3, null, "Horizon Call of the Mountain", 739.0m, new DateTime(2023, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Horizon Call of the Mountain", new TimeSpan(0, 0, 0, 0, 0), null, null, "https://www.playstation.com/sv-se/games/horizon-call-of-the-mountain/" },
                    { 4, new DateTime(2024, 8, 9, 17, 2, 56, 531, DateTimeKind.Local).AddTicks(8427), "System", null, null, 4, null, "Ghost of Tsushima DIRECTOR’S CUT", 0.0m, new DateTime(2022, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2021, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ghost of Tsushima DIRECTOR’S CUT", new TimeSpan(0, 0, 0, 0, 0), null, null, "https://www.playstation.com/en-se/games/ghost-of-tsushima/" }
                });

            migrationBuilder.InsertData(
                table: "ConsoleVideoGame",
                columns: new[] { "Id", "ConsoleId", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "UpdatedAt", "UpdatedBy", "VideoGameId" },
                values: new object[,]
                {
                    { 1, 1, null, null, null, null, null, null, 1 },
                    { 2, 1, null, null, null, null, null, null, 2 },
                    { 3, 1, null, null, null, null, null, null, 4 },
                    { 4, 2, null, null, null, null, null, null, 3 }
                });

            migrationBuilder.InsertData(
                table: "Review",
                columns: new[] { "Id", "ConsoleId", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Grade", "ReviewDate", "ReviewText", "UpdatedAt", "UpdatedBy", "VideoGameId" },
                values: new object[,]
                {
                    { 2, null, new DateTime(2024, 8, 9, 17, 2, 56, 531, DateTimeKind.Local).AddTicks(8150), "System", null, null, 100, new DateTime(2024, 8, 9, 17, 2, 56, 531, DateTimeKind.Local).AddTicks(8149), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", null, null, 1 },
                    { 3, null, new DateTime(2024, 8, 9, 17, 2, 56, 531, DateTimeKind.Local).AddTicks(8153), "System", null, null, 100, new DateTime(2024, 8, 9, 17, 2, 56, 531, DateTimeKind.Local).AddTicks(8152), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", null, null, 1 },
                    { 4, null, new DateTime(2024, 8, 9, 17, 2, 56, 531, DateTimeKind.Local).AddTicks(8157), "System", null, null, 100, new DateTime(2024, 8, 9, 17, 2, 56, 531, DateTimeKind.Local).AddTicks(8156), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", null, null, 1 },
                    { 5, null, new DateTime(2024, 8, 9, 17, 2, 56, 531, DateTimeKind.Local).AddTicks(8161), "System", null, null, 100, new DateTime(2024, 8, 9, 17, 2, 56, 531, DateTimeKind.Local).AddTicks(8159), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", null, null, 3 }
                });

            migrationBuilder.InsertData(
                table: "VideoGameGenre",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "GenreId", "UpdatedAt", "UpdatedBy", "VideoGameId" },
                values: new object[,]
                {
                    { 1, null, null, null, null, 1, null, null, 1 },
                    { 2, null, null, null, null, 5, null, null, 1 },
                    { 3, null, null, null, null, 1, null, null, 2 },
                    { 4, null, null, null, null, 5, null, null, 2 },
                    { 5, null, null, null, null, 1, null, null, 3 },
                    { 6, null, null, null, null, 2, null, null, 3 },
                    { 7, null, null, null, null, 1, null, null, 4 },
                    { 8, null, null, null, null, 5, null, null, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Company_HeadquarterId",
                table: "Company",
                column: "HeadquarterId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Company_ParentCompanyId",
                table: "Company",
                column: "ParentCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Console_DeveloperId",
                table: "Console",
                column: "DeveloperId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsoleVideoGame_ConsoleId",
                table: "ConsoleVideoGame",
                column: "ConsoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsoleVideoGame_VideoGameId",
                table: "ConsoleVideoGame",
                column: "VideoGameId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_ConsoleId",
                table: "Review",
                column: "ConsoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_VideoGameId",
                table: "Review",
                column: "VideoGameId");

            migrationBuilder.CreateIndex(
                name: "IX_Trophy_VideoGameId",
                table: "Trophy",
                column: "VideoGameId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoGame_DeveloperId",
                table: "VideoGame",
                column: "DeveloperId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoGameGenre_GenreId",
                table: "VideoGameGenre",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_VideoGameGenre_VideoGameId",
                table: "VideoGameGenre",
                column: "VideoGameId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConsoleVideoGame");

            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "Trophy");

            migrationBuilder.DropTable(
                name: "VideoGameGenre");

            migrationBuilder.DropTable(
                name: "Console");

            migrationBuilder.DropTable(
                name: "Genre");

            migrationBuilder.DropTable(
                name: "VideoGame");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "Address");
        }
    }
}
