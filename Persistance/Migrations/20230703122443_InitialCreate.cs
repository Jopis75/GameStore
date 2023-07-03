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
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StreetAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    Founded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HeadquartersId = table.Column<int>(type: "int", nullable: true),
                    LogoImageUri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfEmployees = table.Column<int>(type: "int", nullable: true),
                    ParentCompanyId = table.Column<int>(type: "int", nullable: true),
                    TradeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                        name: "FK_Company_Address_HeadquartersId",
                        column: x => x.HeadquartersId,
                        principalTable: "Address",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Company_Company_ParentCompanyId",
                        column: x => x.ParentCompanyId,
                        principalTable: "Company",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoverImageUri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeveloperId = table.Column<int>(type: "int", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReviewId = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TotalTimePlayed = table.Column<TimeSpan>(type: "time", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Company_DeveloperId",
                        column: x => x.DeveloperId,
                        principalTable: "Company",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Grade = table.Column<int>(type: "int", nullable: true),
                    ReviewDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReviewText = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                        name: "FK_Review_Product_VideoGameId",
                        column: x => x.VideoGameId,
                        principalTable: "Product",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "Id", "City", "Country", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "PostalCode", "State", "StreetAddress", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, "San Mateo", "United States", new DateTime(2023, 7, 3, 14, 24, 43, 793, DateTimeKind.Local).AddTicks(4749), "System", null, "", "94404", "California", "2207 Bridgepointe Pkwy", null, "" },
                    { 2, "Amsterdam", "The Netherlands", new DateTime(2023, 7, 3, 14, 24, 43, 793, DateTimeKind.Local).AddTicks(4799), "System", null, "", "1012 RL", "", "Nieuwezijds Voorburgwal 225", null, "" },
                    { 3, "Bellevue", "United States", new DateTime(2023, 7, 3, 14, 24, 43, 793, DateTimeKind.Local).AddTicks(4803), "System", null, "", "98004", "Washington", "500 108th Avenue North East Suite 2600", null, "" }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "CoverImageUri", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "DeveloperId", "Price", "PurchaseDate", "ReleaseDate", "ReviewId", "Title", "TotalTimePlayed", "UpdatedAt", "UpdatedBy", "Url" },
                values: new object[] { 1, "", new DateTime(2023, 7, 3, 14, 24, 43, 794, DateTimeKind.Local).AddTicks(654), "System", null, "", null, 649m, new DateTime(2022, 7, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Horizon Forbidden West", new TimeSpan(0, 0, 0, 0, 0), null, "", "https://www.playstation.com/sv-se/games/horizon-forbidden-west/" });

            migrationBuilder.InsertData(
                table: "Company",
                columns: new[] { "Id", "CompanyType", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Founded", "HeadquartersId", "LogoImageUri", "Name", "NumberOfEmployees", "ParentCompanyId", "TradeName", "UpdatedAt", "UpdatedBy", "WebsiteUrl" },
                values: new object[,]
                {
                    { 1, 2, new DateTime(2023, 7, 3, 14, 24, 43, 794, DateTimeKind.Local).AddTicks(841), "System", null, "", new DateTime(2005, 9, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "", "PlayStation Studios", 4000, null, "PlayStation Studios", null, "", "https://www.playstation.com/en-us/corporate/playstation-studios/" },
                    { 2, 0, new DateTime(2023, 7, 3, 14, 24, 43, 794, DateTimeKind.Local).AddTicks(848), "System", null, "", new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "", "Guerrilla", 360, 1, "Guerrilla Games", null, "", "https://www.guerrilla-games.com/" },
                    { 3, 0, new DateTime(2023, 7, 3, 14, 24, 43, 794, DateTimeKind.Local).AddTicks(854), "System", null, "", new DateTime(1997, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "", "Sucker Punch", 160, 1, "Sucker Punch Productions", null, "", "https://www.suckerpunch.com/" }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "CoverImageUri", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "DeveloperId", "Price", "PurchaseDate", "ReleaseDate", "ReviewId", "Title", "TotalTimePlayed", "UpdatedAt", "UpdatedBy", "Url" },
                values: new object[,]
                {
                    { 2, "", new DateTime(2023, 7, 3, 14, 24, 43, 794, DateTimeKind.Local).AddTicks(664), "System", null, "", 2, 739m, new DateTime(2023, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Horizon Call of the Mountain", new TimeSpan(0, 0, 0, 0, 0), null, "", "https://www.playstation.com/en-se/games/horizon-call-of-the-mountain/" },
                    { 3, "", new DateTime(2023, 7, 3, 14, 24, 43, 794, DateTimeKind.Local).AddTicks(673), "System", null, "", 3, 699m, new DateTime(2022, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Ghost of Tsushima", new TimeSpan(0, 0, 0, 0, 0), null, "", "https://www.playstation.com/en-se/games/ghost-of-tsushima/" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Company_HeadquartersId",
                table: "Company",
                column: "HeadquartersId");

            migrationBuilder.CreateIndex(
                name: "IX_Company_ParentCompanyId",
                table: "Company",
                column: "ParentCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_DeveloperId",
                table: "Product",
                column: "DeveloperId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_VideoGameId",
                table: "Review",
                column: "VideoGameId",
                unique: true,
                filter: "[VideoGameId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Review");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropTable(
                name: "Address");
        }
    }
}
