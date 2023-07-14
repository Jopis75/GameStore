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
                    CompanyType = table.Column<int>(type: "int", nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HeadquartersId = table.Column<int>(type: "int", nullable: true),
                    Industry = table.Column<int>(type: "int", nullable: true),
                    LogoImageUri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentCompanyId = table.Column<int>(type: "int", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
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
                    DeveloperId = table.Column<int>(type: "int", nullable: true),
                    ImageUri = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConsoleId = table.Column<int>(type: "int", nullable: true),
                    Grade = table.Column<int>(type: "int", nullable: true),
                    ReviewDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReviewText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true),
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
                        name: "FK_Review_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Console",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeveloperId = table.Column<int>(type: "int", nullable: true),
                    ImageUri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReviewId = table.Column<int>(type: "int", nullable: true),
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
                    table.PrimaryKey("PK_Console", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Console_Company_DeveloperId",
                        column: x => x.DeveloperId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Console_Review_ReviewId",
                        column: x => x.ReviewId,
                        principalTable: "Review",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ConsoleProduct",
                columns: table => new
                {
                    ConsoleId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Id = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsoleProduct", x => new { x.ConsoleId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_ConsoleProduct_Console_ConsoleId",
                        column: x => x.ConsoleId,
                        principalTable: "Console",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConsoleProduct_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "Id", "City", "Country", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "PostalCode", "State", "StreetAddress", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, "San Mateo", "USA", new DateTime(2023, 7, 14, 16, 11, 37, 787, DateTimeKind.Local).AddTicks(7486), "System", null, "", "94404", "California", "2207 Bridgepointe Pkwy", null, "" },
                    { 2, "San Mateo", "United States", new DateTime(2023, 7, 14, 16, 11, 37, 787, DateTimeKind.Local).AddTicks(7535), "System", null, "", "94404", "California", "2207 Bridgepointe Pkwy", null, "" },
                    { 3, "Amsterdam", "The Netherlands", new DateTime(2023, 7, 14, 16, 11, 37, 787, DateTimeKind.Local).AddTicks(7539), "System", null, "", "1012 RL", "", "Nieuwezijds Voorburgwal 225", null, "" },
                    { 4, "Bellevue", "United States", new DateTime(2023, 7, 14, 16, 11, 37, 787, DateTimeKind.Local).AddTicks(7542), "System", null, "", "98004", "Washington", "500 108th Avenue North East Suite 2600", null, "" }
                });

            migrationBuilder.InsertData(
                table: "Company",
                columns: new[] { "Id", "CompanyType", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "EmailAddress", "HeadquartersId", "Industry", "LogoImageUri", "Name", "ParentCompanyId", "PhoneNumber", "TradeName", "UpdatedAt", "UpdatedBy", "WebsiteUrl" },
                values: new object[,]
                {
                    { 1, 0, new DateTime(2023, 7, 14, 16, 11, 37, 787, DateTimeKind.Local).AddTicks(8316), "System", null, "", null, 1, null, "", "Sony Interactive Entertainment", null, null, "Sony Interactive Entertainment", null, "", "https://sonyinteractive.com/en/" },
                    { 2, 2, new DateTime(2023, 7, 14, 16, 11, 37, 787, DateTimeKind.Local).AddTicks(8321), "System", null, "", null, 2, null, "", "PlayStation Studios", 2, null, "PlayStation Studios", null, "", "https://www.playstation.com/en-us/corporate/playstation-studios/" },
                    { 3, 0, new DateTime(2023, 7, 14, 16, 11, 37, 787, DateTimeKind.Local).AddTicks(8324), "System", null, "", null, 3, null, "", "Guerrilla", 2, null, "Guerrilla Games", null, "", "https://www.guerrilla-games.com/" },
                    { 4, 0, new DateTime(2023, 7, 14, 16, 11, 37, 787, DateTimeKind.Local).AddTicks(8327), "System", null, "", null, 4, null, "", "Sucker Punch", 2, null, "Sucker Punch Productions", null, "", "https://www.suckerpunch.com/" }
                });

            migrationBuilder.InsertData(
                table: "Console",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "DeveloperId", "ImageUri", "Name", "Price", "PurchaseDate", "ReleaseDate", "ReviewId", "UpdatedAt", "UpdatedBy", "Url" },
                values: new object[] { 1, new DateTime(2023, 7, 14, 16, 11, 37, 787, DateTimeKind.Local).AddTicks(7835), "System", null, "", 1, "", "PlayStation®5 Console", 499.99m, new DateTime(2023, 7, 14, 16, 11, 37, 787, DateTimeKind.Local).AddTicks(7832), new DateTime(2020, 11, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "", "https://direct.playstation.com/en-us/buy-consoles/playstation5-console/" });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "DeveloperId", "ImageUri", "Price", "PurchaseDate", "ReleaseDate", "ReviewId", "Title", "TotalTimePlayed", "UpdatedAt", "UpdatedBy", "Url" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 7, 14, 16, 11, 37, 787, DateTimeKind.Local).AddTicks(7999), "System", null, "", 2, "", 69.99m, new DateTime(2022, 7, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Horizon Forbidden West", new TimeSpan(0, 0, 0, 0, 0), null, "", "https://www.playstation.com/sv-se/games/horizon-forbidden-west/" },
                    { 2, new DateTime(2023, 7, 14, 16, 11, 37, 787, DateTimeKind.Local).AddTicks(8007), "System", null, "", 2, "", 59.99m, new DateTime(2023, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Horizon Call of the Mountain", new TimeSpan(0, 0, 0, 0, 0), null, "", "https://www.playstation.com/en-se/games/horizon-call-of-the-mountain/" }
                });

            migrationBuilder.InsertData(
                table: "ConsoleProduct",
                columns: new[] { "ConsoleId", "ProductId", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Id", "UpdatedAt", "UpdatedBy" },
                values: new object[,]
                {
                    { 1, 1, null, null, null, null, 0, null, null },
                    { 1, 2, null, null, null, null, 0, null, null }
                });

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "DeveloperId", "ImageUri", "Price", "PurchaseDate", "ReleaseDate", "ReviewId", "Title", "TotalTimePlayed", "UpdatedAt", "UpdatedBy", "Url" },
                values: new object[] { 3, new DateTime(2023, 7, 14, 16, 11, 37, 787, DateTimeKind.Local).AddTicks(8015), "System", null, "", 3, "", 69.99m, new DateTime(2022, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2020, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Ghost of Tsushima DIRECTOR’S CUT", new TimeSpan(0, 0, 0, 0, 0), null, "", "https://www.playstation.com/en-se/games/ghost-of-tsushima/" });

            migrationBuilder.InsertData(
                table: "ConsoleProduct",
                columns: new[] { "ConsoleId", "ProductId", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Id", "UpdatedAt", "UpdatedBy" },
                values: new object[] { 1, 3, null, null, null, null, 0, null, null });

            migrationBuilder.CreateIndex(
                name: "IX_Company_HeadquartersId",
                table: "Company",
                column: "HeadquartersId",
                unique: true,
                filter: "[HeadquartersId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Company_ParentCompanyId",
                table: "Company",
                column: "ParentCompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Console_DeveloperId",
                table: "Console",
                column: "DeveloperId");

            migrationBuilder.CreateIndex(
                name: "IX_Console_ReviewId",
                table: "Console",
                column: "ReviewId",
                unique: true,
                filter: "[ReviewId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ConsoleProduct_ProductId",
                table: "ConsoleProduct",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_DeveloperId",
                table: "Product",
                column: "DeveloperId");

            migrationBuilder.CreateIndex(
                name: "IX_Review_ProductId",
                table: "Review",
                column: "ProductId",
                unique: true,
                filter: "[ProductId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConsoleProduct");

            migrationBuilder.DropTable(
                name: "Console");

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
