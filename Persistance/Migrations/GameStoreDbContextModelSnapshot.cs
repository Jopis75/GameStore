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
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
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
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetAddress")
                        .IsRequired()
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
                            CreatedAt = new DateTime(2023, 12, 26, 14, 27, 49, 684, DateTimeKind.Local).AddTicks(4646),
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
                            CreatedAt = new DateTime(2023, 12, 26, 14, 27, 49, 684, DateTimeKind.Local).AddTicks(4691),
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
                            CreatedAt = new DateTime(2023, 12, 26, 14, 27, 49, 684, DateTimeKind.Local).AddTicks(4695),
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
                            CreatedAt = new DateTime(2023, 12, 26, 14, 27, 49, 684, DateTimeKind.Local).AddTicks(4697),
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

                    b.Property<int>("CompanyType")
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
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HeadquarterId")
                        .HasColumnType("int");

                    b.Property<int>("Industry")
                        .HasColumnType("int");

                    b.Property<string>("LogoImageUri")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ParentCompanyId")
                        .HasColumnType("int");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TradeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WebsiteUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("HeadquarterId")
                        .IsUnique();

                    b.HasIndex("ParentCompanyId");

                    b.ToTable("Company", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CompanyType = 0,
                            CreatedAt = new DateTime(2023, 12, 26, 14, 27, 49, 684, DateTimeKind.Local).AddTicks(5637),
                            CreatedBy = "System",
                            DeletedBy = "",
                            EmailAddress = "johan.steinrud@gmail.com",
                            HeadquarterId = 1,
                            Industry = 0,
                            LogoImageUri = "",
                            Name = "Sony Interactive Entertainment",
                            PhoneNumber = "46702651007",
                            TradeName = "Sony Interactive Entertainment",
                            UpdatedBy = "",
                            WebsiteUrl = "https://sonyinteractive.com/en/"
                        },
                        new
                        {
                            Id = 2,
                            CompanyType = 2,
                            CreatedAt = new DateTime(2023, 12, 26, 14, 27, 49, 684, DateTimeKind.Local).AddTicks(5643),
                            CreatedBy = "System",
                            DeletedBy = "",
                            EmailAddress = "johan.steinrud@gmail.com",
                            HeadquarterId = 2,
                            Industry = 0,
                            LogoImageUri = "",
                            Name = "PlayStation Studios",
                            ParentCompanyId = 2,
                            PhoneNumber = "46702651007",
                            TradeName = "PlayStation Studios",
                            UpdatedBy = "",
                            WebsiteUrl = "https://www.playstation.com/en-us/corporate/playstation-studios/"
                        },
                        new
                        {
                            Id = 3,
                            CompanyType = 0,
                            CreatedAt = new DateTime(2023, 12, 26, 14, 27, 49, 684, DateTimeKind.Local).AddTicks(5646),
                            CreatedBy = "System",
                            DeletedBy = "",
                            EmailAddress = "johan.steinrud@gmail.com",
                            HeadquarterId = 3,
                            Industry = 0,
                            LogoImageUri = "",
                            Name = "Guerrilla",
                            ParentCompanyId = 2,
                            PhoneNumber = "46702651007",
                            TradeName = "Guerrilla Games",
                            UpdatedBy = "",
                            WebsiteUrl = "https://www.guerrilla-games.com/"
                        },
                        new
                        {
                            Id = 4,
                            CompanyType = 0,
                            CreatedAt = new DateTime(2023, 12, 26, 14, 27, 49, 684, DateTimeKind.Local).AddTicks(5649),
                            CreatedBy = "System",
                            DeletedBy = "",
                            EmailAddress = "johan.steinrud@gmail.com",
                            HeadquarterId = 4,
                            Industry = 0,
                            LogoImageUri = "",
                            Name = "Sucker Punch",
                            ParentCompanyId = 2,
                            PhoneNumber = "46702651007",
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

                    b.Property<int>("DeveloperId")
                        .HasColumnType("int");

                    b.Property<string>("ImageUri")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("PurchaseDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ReviewId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .IsRequired()
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
                            CreatedAt = new DateTime(2023, 12, 26, 14, 27, 49, 684, DateTimeKind.Local).AddTicks(5044),
                            CreatedBy = "System",
                            DeletedBy = "",
                            DeveloperId = 1,
                            ImageUri = "",
                            Name = "PlayStation®5 Console",
                            Price = 9988.00m,
                            PurchaseDate = new DateTime(2022, 1, 29, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ReleaseDate = new DateTime(2020, 11, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UpdatedBy = "",
                            Url = "https://direct.playstation.com/en-us/buy-consoles/playstation5-console/"
                        });
                });

            modelBuilder.Entity("Domain.Entities.ConsoleVideoGame", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ConsoleId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VideoGameId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ConsoleId");

                    b.HasIndex("VideoGameId");

                    b.ToTable("ConsoleVideoGame", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ConsoleId = 1,
                            VideoGameId = 1
                        },
                        new
                        {
                            Id = 2,
                            ConsoleId = 1,
                            VideoGameId = 2
                        },
                        new
                        {
                            Id = 3,
                            ConsoleId = 1,
                            VideoGameId = 3
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

                    b.Property<int>("Grade")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReviewDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ReviewText")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("VideoGameId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VideoGameId")
                        .IsUnique()
                        .HasFilter("[VideoGameId] IS NOT NULL");

                    b.ToTable("Review", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.VideoGame", b =>
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

                    b.Property<int>("DeveloperId")
                        .HasColumnType("int");

                    b.Property<string>("ImageUri")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("PurchaseDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ReviewId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<TimeSpan>("TotalTimePlayed")
                        .HasColumnType("time");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DeveloperId");

                    b.ToTable("VideoGame", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateTime(2023, 12, 26, 14, 27, 49, 684, DateTimeKind.Local).AddTicks(5224),
                            CreatedBy = "System",
                            DeletedBy = "",
                            DeveloperId = 2,
                            ImageUri = "",
                            Name = "Horizon Forbidden West",
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
                            CreatedAt = new DateTime(2023, 12, 26, 14, 27, 49, 684, DateTimeKind.Local).AddTicks(5233),
                            CreatedBy = "System",
                            DeletedBy = "",
                            DeveloperId = 2,
                            ImageUri = "",
                            Name = "Horizon Call of the Mountain",
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
                            CreatedAt = new DateTime(2023, 12, 26, 14, 27, 49, 684, DateTimeKind.Local).AddTicks(5241),
                            CreatedBy = "System",
                            DeletedBy = "",
                            DeveloperId = 3,
                            ImageUri = "",
                            Name = "Ghost of Tsushima DIRECTOR’S CUT",
                            Price = 69.99m,
                            PurchaseDate = new DateTime(2022, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ReleaseDate = new DateTime(2020, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "Ghost of Tsushima DIRECTOR’S CUT",
                            TotalTimePlayed = new TimeSpan(0, 0, 0, 0, 0),
                            UpdatedBy = "",
                            Url = "https://www.playstation.com/en-se/games/ghost-of-tsushima/"
                        });
                });

            modelBuilder.Entity("Domain.Entities.Company", b =>
                {
                    b.HasOne("Domain.Entities.Address", "Headquarter")
                        .WithOne("Company")
                        .HasForeignKey("Domain.Entities.Company", "HeadquarterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Company", "ParentCompany")
                        .WithMany()
                        .HasForeignKey("ParentCompanyId");

                    b.Navigation("Headquarter");

                    b.Navigation("ParentCompany");
                });

            modelBuilder.Entity("Domain.Entities.Console", b =>
                {
                    b.HasOne("Domain.Entities.Company", "Developer")
                        .WithMany("Consoles")
                        .HasForeignKey("DeveloperId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Review", "Review")
                        .WithOne("Console")
                        .HasForeignKey("Domain.Entities.Console", "ReviewId");

                    b.Navigation("Developer");

                    b.Navigation("Review");
                });

            modelBuilder.Entity("Domain.Entities.ConsoleVideoGame", b =>
                {
                    b.HasOne("Domain.Entities.Console", "Console")
                        .WithMany("ConsoleVideoGames")
                        .HasForeignKey("ConsoleId")
                        .IsRequired();

                    b.HasOne("Domain.Entities.VideoGame", "VideoGame")
                        .WithMany("ConsoleVideoGames")
                        .HasForeignKey("VideoGameId")
                        .IsRequired();

                    b.Navigation("Console");

                    b.Navigation("VideoGame");
                });

            modelBuilder.Entity("Domain.Entities.Review", b =>
                {
                    b.HasOne("Domain.Entities.VideoGame", "VideoGame")
                        .WithOne("Review")
                        .HasForeignKey("Domain.Entities.Review", "VideoGameId");

                    b.Navigation("VideoGame");
                });

            modelBuilder.Entity("Domain.Entities.VideoGame", b =>
                {
                    b.HasOne("Domain.Entities.Company", "Developer")
                        .WithMany("VideoGames")
                        .HasForeignKey("DeveloperId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Developer");
                });

            modelBuilder.Entity("Domain.Entities.Address", b =>
                {
                    b.Navigation("Company");
                });

            modelBuilder.Entity("Domain.Entities.Company", b =>
                {
                    b.Navigation("Consoles");

                    b.Navigation("VideoGames");
                });

            modelBuilder.Entity("Domain.Entities.Console", b =>
                {
                    b.Navigation("ConsoleVideoGames");
                });

            modelBuilder.Entity("Domain.Entities.Review", b =>
                {
                    b.Navigation("Console");
                });

            modelBuilder.Entity("Domain.Entities.VideoGame", b =>
                {
                    b.Navigation("ConsoleVideoGames");

                    b.Navigation("Review");
                });
#pragma warning restore 612, 618
        }
    }
}
