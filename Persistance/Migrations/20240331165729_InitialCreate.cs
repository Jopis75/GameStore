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

            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "Id", "City", "Country", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "PostalCode", "State", "StreetAddress", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, "San Mateo", "USA", new DateTime(2024, 3, 31, 18, 57, 29, 791, DateTimeKind.Local).AddTicks(6901), "System", null, "", "94404", "California", "2207 Bridgepointe Pkwy", null, "" },
                    { 2, "San Mateo", "United States", new DateTime(2024, 3, 31, 18, 57, 29, 791, DateTimeKind.Local).AddTicks(6948), "System", null, "", "94404", "California", "2207 Bridgepointe Pkwy", null, "" },
                    { 3, "Amsterdam", "The Netherlands", new DateTime(2024, 3, 31, 18, 57, 29, 791, DateTimeKind.Local).AddTicks(6951), "System", null, "", "1012 RL", "", "Nieuwezijds Voorburgwal 225", null, "" },
                    { 4, "Bellevue", "United States", new DateTime(2024, 3, 31, 18, 57, 29, 791, DateTimeKind.Local).AddTicks(6954), "System", null, "", "98004", "Washington", "500 108th Avenue North East Suite 2600", null, "" }
                });

            migrationBuilder.InsertData(
                table: "Company",
                columns: new[] { "Id", "CompanyType", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "EmailAddress", "HeadquarterId", "Industry", "LogoImageUri", "Name", "ParentCompanyId", "PhoneNumber", "TradeName", "UpdatedAt", "UpdatedBy", "WebsiteUrl" },
                values: new object[,]
                {
                    { 1, 0, new DateTime(2024, 3, 31, 18, 57, 29, 791, DateTimeKind.Local).AddTicks(7765), "System", null, "", "johan.steinrud@gmail.com", 1, 0, "", "Sony Interactive Entertainment", null, "46702651007", "Sony Interactive Entertainment", null, "", "https://sonyinteractive.com/en/" },
                    { 2, 2, new DateTime(2024, 3, 31, 18, 57, 29, 791, DateTimeKind.Local).AddTicks(7770), "System", null, "", "johan.steinrud@gmail.com", 2, 0, "", "PlayStation Studios", 2, "46702651007", "PlayStation Studios", null, "", "https://www.playstation.com/en-us/corporate/playstation-studios/" },
                    { 3, 0, new DateTime(2024, 3, 31, 18, 57, 29, 791, DateTimeKind.Local).AddTicks(7773), "System", null, "", "johan.steinrud@gmail.com", 3, 0, "", "Guerrilla", 2, "46702651007", "Guerrilla Games", null, "", "https://www.guerrilla-games.com/" },
                    { 4, 0, new DateTime(2024, 3, 31, 18, 57, 29, 791, DateTimeKind.Local).AddTicks(7776), "System", null, "", "johan.steinrud@gmail.com", 4, 0, "", "Sucker Punch", 2, "46702651007", "Sucker Punch Productions", null, "", "https://www.suckerpunch.com/" }
                });

            migrationBuilder.InsertData(
                table: "Console",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "DeveloperId", "ImageUri", "Name", "Price", "PurchaseDate", "ReleaseDate", "UpdatedAt", "UpdatedBy", "Url" },
                values: new object[] { 1, new DateTime(2024, 3, 31, 18, 57, 29, 791, DateTimeKind.Local).AddTicks(7272), "System", null, "", 1, "", "PlayStation®5 Console", 9988.00m, new DateTime(2022, 1, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 11, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "", "https://direct.playstation.com/en-us/buy-consoles/playstation5-console/" });

            migrationBuilder.InsertData(
                table: "VideoGame",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "DeveloperId", "ImageUri", "Name", "Price", "PurchaseDate", "ReleaseDate", "Title", "TotalTimePlayed", "UpdatedAt", "UpdatedBy", "Url" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 3, 31, 18, 57, 29, 791, DateTimeKind.Local).AddTicks(7438), "System", null, "", 2, "", "Horizon Forbidden West", 69.99m, new DateTime(2022, 7, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Horizon Forbidden West", new TimeSpan(0, 0, 0, 0, 0), null, "", "https://www.playstation.com/sv-se/games/horizon-forbidden-west/" },
                    { 2, new DateTime(2024, 3, 31, 18, 57, 29, 791, DateTimeKind.Local).AddTicks(7446), "System", null, "", 2, "", "Horizon Call of the Mountain", 59.99m, new DateTime(2023, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Horizon Call of the Mountain", new TimeSpan(0, 0, 0, 0, 0), null, "", "https://www.playstation.com/en-se/games/horizon-call-of-the-mountain/" }
                });

            migrationBuilder.InsertData(
                table: "ConsoleVideoGame",
                columns: new[] { "Id", "ConsoleId", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "UpdatedAt", "UpdatedBy", "VideoGameId" },
                values: new object[,]
                {
                    { 1, 1, null, null, null, null, null, null, 1 },
                    { 2, 1, null, null, null, null, null, null, 2 }
                });

            migrationBuilder.InsertData(
                table: "Review",
                columns: new[] { "Id", "ConsoleId", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Grade", "ReviewDate", "ReviewText", "UpdatedAt", "UpdatedBy", "VideoGameId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 3, 31, 18, 57, 29, 791, DateTimeKind.Local).AddTicks(7959), "System", null, "", 100, new DateTime(2024, 3, 31, 18, 57, 29, 791, DateTimeKind.Local).AddTicks(7957), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", null, "", null },
                    { 2, null, new DateTime(2024, 3, 31, 18, 57, 29, 791, DateTimeKind.Local).AddTicks(7964), "System", null, "", 100, new DateTime(2024, 3, 31, 18, 57, 29, 791, DateTimeKind.Local).AddTicks(7962), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", null, "", 1 },
                    { 3, null, new DateTime(2024, 3, 31, 18, 57, 29, 791, DateTimeKind.Local).AddTicks(7967), "System", null, "", 100, new DateTime(2024, 3, 31, 18, 57, 29, 791, DateTimeKind.Local).AddTicks(7966), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", null, "", 1 },
                    { 4, null, new DateTime(2024, 3, 31, 18, 57, 29, 791, DateTimeKind.Local).AddTicks(7971), "System", null, "", 100, new DateTime(2024, 3, 31, 18, 57, 29, 791, DateTimeKind.Local).AddTicks(7969), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", null, "", 1 }
                });

            migrationBuilder.InsertData(
                table: "VideoGame",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "DeveloperId", "ImageUri", "Name", "Price", "PurchaseDate", "ReleaseDate", "Title", "TotalTimePlayed", "UpdatedAt", "UpdatedBy", "Url" },
                values: new object[] { 3, new DateTime(2024, 3, 31, 18, 57, 29, 791, DateTimeKind.Local).AddTicks(7453), "System", null, "", 3, "", "Ghost of Tsushima DIRECTOR’S CUT", 69.99m, new DateTime(2022, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ghost of Tsushima DIRECTOR’S CUT", new TimeSpan(0, 0, 0, 0, 0), null, "", "https://www.playstation.com/en-se/games/ghost-of-tsushima/" });

            migrationBuilder.InsertData(
                table: "ConsoleVideoGame",
                columns: new[] { "Id", "ConsoleId", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "UpdatedAt", "UpdatedBy", "VideoGameId" },
                values: new object[] { 3, 1, null, null, null, null, null, null, 3 });

            migrationBuilder.InsertData(
                table: "Review",
                columns: new[] { "Id", "ConsoleId", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Grade", "ReviewDate", "ReviewText", "UpdatedAt", "UpdatedBy", "VideoGameId" },
                values: new object[] { 5, null, new DateTime(2024, 3, 31, 18, 57, 29, 791, DateTimeKind.Local).AddTicks(7974), "System", null, "", 100, new DateTime(2024, 3, 31, 18, 57, 29, 791, DateTimeKind.Local).AddTicks(7973), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.", null, "", 3 });

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
                name: "IX_VideoGame_DeveloperId",
                table: "VideoGame",
                column: "DeveloperId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConsoleVideoGame");

            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "Console");

            migrationBuilder.DropTable(
                name: "VideoGame");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "Address");
        }
    }
}
