﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistance.DbContexts;

#nullable disable

namespace Persistance.Migrations
{
    [DbContext(typeof(GameStoreDbContext))]
    partial class GameStoreDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Address", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            City = "San Mateo",
                            Country = "USA",
                            CreatedAt = new DateTime(2023, 7, 5, 14, 10, 10, 403, DateTimeKind.Local).AddTicks(9965),
                            CreatedBy = "System",
                            DeletedBy = "",
                            PostalCode = "94404",
                            State = "California",
                            StreetAddress = "2207 Bridgepointe Pkwy",
                            UpdatedBy = ""
                        },
                        new
                        {
                            Id = 2,
                            City = "San Mateo",
                            Country = "United States",
                            CreatedAt = new DateTime(2023, 7, 5, 14, 10, 10, 404, DateTimeKind.Local).AddTicks(15),
                            CreatedBy = "System",
                            DeletedBy = "",
                            PostalCode = "94404",
                            State = "California",
                            StreetAddress = "2207 Bridgepointe Pkwy",
                            UpdatedBy = ""
                        },
                        new
                        {
                            Id = 3,
                            City = "Amsterdam",
                            Country = "The Netherlands",
                            CreatedAt = new DateTime(2023, 7, 5, 14, 10, 10, 404, DateTimeKind.Local).AddTicks(18),
                            CreatedBy = "System",
                            DeletedBy = "",
                            PostalCode = "1012 RL",
                            State = "",
                            StreetAddress = "Nieuwezijds Voorburgwal 225",
                            UpdatedBy = ""
                        },
                        new
                        {
                            Id = 4,
                            City = "Bellevue",
                            Country = "United States",
                            CreatedAt = new DateTime(2023, 7, 5, 14, 10, 10, 404, DateTimeKind.Local).AddTicks(21),
                            CreatedBy = "System",
                            DeletedBy = "",
                            PostalCode = "98004",
                            State = "Washington",
                            StreetAddress = "500 108th Avenue North East Suite 2600",
                            UpdatedBy = ""
                        });
                });

            modelBuilder.Entity("Domain.Entities.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CompanyType")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmailAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("HeadquartersId")
                        .HasColumnType("int");

                    b.Property<int?>("Industry")
                        .HasColumnType("int");

                    b.Property<string>("LogoImageUri")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ParentCompanyId")
                        .HasColumnType("int");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TradeName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WebsiteUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("HeadquartersId");

                    b.HasIndex("ParentCompanyId");

                    b.ToTable("Company", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CompanyType = 0,
                            CreatedAt = new DateTime(2023, 7, 5, 14, 10, 10, 404, DateTimeKind.Local).AddTicks(870),
                            CreatedBy = "System",
                            DeletedBy = "",
                            HeadquartersId = 1,
                            LogoImageUri = "",
                            Name = "Sony Interactive Entertainment",
                            TradeName = "Sony Interactive Entertainment",
                            UpdatedBy = "",
                            WebsiteUrl = "https://sonyinteractive.com/en/"
                        },
                        new
                        {
                            Id = 2,
                            CompanyType = 2,
                            CreatedAt = new DateTime(2023, 7, 5, 14, 10, 10, 404, DateTimeKind.Local).AddTicks(875),
                            CreatedBy = "System",
                            DeletedBy = "",
                            HeadquartersId = 2,
                            LogoImageUri = "",
                            Name = "PlayStation Studios",
                            ParentCompanyId = 2,
                            TradeName = "PlayStation Studios",
                            UpdatedBy = "",
                            WebsiteUrl = "https://www.playstation.com/en-us/corporate/playstation-studios/"
                        },
                        new
                        {
                            Id = 3,
                            CompanyType = 0,
                            CreatedAt = new DateTime(2023, 7, 5, 14, 10, 10, 404, DateTimeKind.Local).AddTicks(878),
                            CreatedBy = "System",
                            DeletedBy = "",
                            HeadquartersId = 3,
                            LogoImageUri = "",
                            Name = "Guerrilla",
                            ParentCompanyId = 2,
                            TradeName = "Guerrilla Games",
                            UpdatedBy = "",
                            WebsiteUrl = "https://www.guerrilla-games.com/"
                        },
                        new
                        {
                            Id = 4,
                            CompanyType = 0,
                            CreatedAt = new DateTime(2023, 7, 5, 14, 10, 10, 404, DateTimeKind.Local).AddTicks(881),
                            CreatedBy = "System",
                            DeletedBy = "",
                            HeadquartersId = 4,
                            LogoImageUri = "",
                            Name = "Sucker Punch",
                            ParentCompanyId = 2,
                            TradeName = "Sucker Punch Productions",
                            UpdatedBy = "",
                            WebsiteUrl = "https://www.suckerpunch.com/"
                        });
                });

            modelBuilder.Entity("Domain.Entities.Console", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("DeveloperId")
                        .HasColumnType("int");

                    b.Property<string>("ImageUri")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Price")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("PurchaseDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ReviewId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DeveloperId");

                    b.HasIndex("ReviewId")
                        .IsUnique()
                        .HasFilter("[ReviewId] IS NOT NULL");

                    b.ToTable("Console", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2023, 7, 5, 14, 10, 10, 404, DateTimeKind.Local).AddTicks(370),
                            CreatedBy = "System",
                            DeletedBy = "",
                            DeveloperId = 1,
                            ImageUri = "",
                            Name = "PlayStation®5 Console",
                            Price = 499.99m,
                            PurchaseDate = new DateTime(2023, 7, 5, 14, 10, 10, 404, DateTimeKind.Local).AddTicks(367),
                            ReleaseDate = new DateTime(2020, 11, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UpdatedBy = "",
                            Url = "https://direct.playstation.com/en-us/buy-consoles/playstation5-console/"
                        });
                });

            modelBuilder.Entity("Domain.Entities.ConsoleProduct", b =>
                {
                    b.Property<int?>("ConsoleId")
                        .HasColumnType("int");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ConsoleId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("ConsoleProducts");

                    b.HasData(
                        new
                        {
                            ConsoleId = 1,
                            ProductId = 1,
                            Id = 0
                        },
                        new
                        {
                            ConsoleId = 1,
                            ProductId = 2,
                            Id = 0
                        },
                        new
                        {
                            ConsoleId = 1,
                            ProductId = 3,
                            Id = 0
                        });
                });

            modelBuilder.Entity("Domain.Entities.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("DeveloperId")
                        .HasColumnType("int");

                    b.Property<string>("ImageUri")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Price")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime?>("PurchaseDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ReviewId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan?>("TotalTimePlayed")
                        .HasColumnType("time");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DeveloperId");

                    b.ToTable("Product", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2023, 7, 5, 14, 10, 10, 404, DateTimeKind.Local).AddTicks(541),
                            CreatedBy = "System",
                            DeletedBy = "",
                            DeveloperId = 2,
                            ImageUri = "",
                            Price = 69.99m,
                            PurchaseDate = new DateTime(2022, 7, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ReleaseDate = new DateTime(2022, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Horizon Forbidden West",
                            TotalTimePlayed = new TimeSpan(0, 0, 0, 0, 0),
                            UpdatedBy = "",
                            Url = "https://www.playstation.com/sv-se/games/horizon-forbidden-west/"
                        },
                        new
                        {
                            Id = 2,
                            CreatedAt = new DateTime(2023, 7, 5, 14, 10, 10, 404, DateTimeKind.Local).AddTicks(550),
                            CreatedBy = "System",
                            DeletedBy = "",
                            DeveloperId = 2,
                            ImageUri = "",
                            Price = 59.99m,
                            PurchaseDate = new DateTime(2023, 2, 24, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ReleaseDate = new DateTime(2023, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Horizon Call of the Mountain",
                            TotalTimePlayed = new TimeSpan(0, 0, 0, 0, 0),
                            UpdatedBy = "",
                            Url = "https://www.playstation.com/en-se/games/horizon-call-of-the-mountain/"
                        },
                        new
                        {
                            Id = 3,
                            CreatedAt = new DateTime(2023, 7, 5, 14, 10, 10, 404, DateTimeKind.Local).AddTicks(558),
                            CreatedBy = "System",
                            DeletedBy = "",
                            DeveloperId = 3,
                            ImageUri = "",
                            Price = 69.99m,
                            PurchaseDate = new DateTime(2022, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ReleaseDate = new DateTime(2020, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Ghost of Tsushima DIRECTOR’S CUT",
                            TotalTimePlayed = new TimeSpan(0, 0, 0, 0, 0),
                            UpdatedBy = "",
                            Url = "https://www.playstation.com/en-se/games/ghost-of-tsushima/"
                        });
                });

            modelBuilder.Entity("Domain.Entities.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("ConsoleId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Grade")
                        .HasColumnType("int");

                    b.Property<int?>("ProductId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ReviewDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ReviewText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId")
                        .IsUnique()
                        .HasFilter("[ProductId] IS NOT NULL");

                    b.ToTable("Review", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Company", b =>
                {
                    b.HasOne("Domain.Entities.Address", "Headquarters")
                        .WithMany()
                        .HasForeignKey("HeadquartersId");

                    b.HasOne("Domain.Entities.Company", "ParentCompany")
                        .WithMany()
                        .HasForeignKey("ParentCompanyId");

                    b.Navigation("Headquarters");

                    b.Navigation("ParentCompany");
                });

            modelBuilder.Entity("Domain.Entities.Console", b =>
                {
                    b.HasOne("Domain.Entities.Company", "Developer")
                        .WithMany("Consoles")
                        .HasForeignKey("DeveloperId");

                    b.HasOne("Domain.Entities.Review", "Review")
                        .WithOne("Console")
                        .HasForeignKey("Domain.Entities.Console", "ReviewId");

                    b.Navigation("Developer");

                    b.Navigation("Review");
                });

            modelBuilder.Entity("Domain.Entities.ConsoleProduct", b =>
                {
                    b.HasOne("Domain.Entities.Console", "Console")
                        .WithMany("ConsoleProducts")
                        .HasForeignKey("ConsoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Product", "Product")
                        .WithMany("ConsoleProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Console");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Domain.Entities.Product", b =>
                {
                    b.HasOne("Domain.Entities.Company", "Developer")
                        .WithMany("Products")
                        .HasForeignKey("DeveloperId");

                    b.Navigation("Developer");
                });

            modelBuilder.Entity("Domain.Entities.Review", b =>
                {
                    b.HasOne("Domain.Entities.Product", "Product")
                        .WithOne("Review")
                        .HasForeignKey("Domain.Entities.Review", "ProductId");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Domain.Entities.Company", b =>
                {
                    b.Navigation("Consoles");

                    b.Navigation("Products");
                });

            modelBuilder.Entity("Domain.Entities.Console", b =>
                {
                    b.Navigation("ConsoleProducts");
                });

            modelBuilder.Entity("Domain.Entities.Product", b =>
                {
                    b.Navigation("ConsoleProducts");

                    b.Navigation("Review");
                });

            modelBuilder.Entity("Domain.Entities.Review", b =>
                {
                    b.Navigation("Console");
                });
#pragma warning restore 612, 618
        }
    }
}
