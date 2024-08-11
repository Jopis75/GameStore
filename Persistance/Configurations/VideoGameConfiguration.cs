using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Configurations
{
    public class VideoGameConfiguration : IEntityTypeConfiguration<VideoGame>
    {
        public void Configure(EntityTypeBuilder<VideoGame> builder)
        {
            builder.HasData(
                new VideoGame
                {
                    Id = 1,
                    Name = "Horizon Zero Dawn - Complete Edition",
                    Title = "Horizon Zero Dawn - Complete Edition",
                    ImageUri = null,
                    DeveloperId = 3,
                    Price = 229.0M,
                    ReleaseDate = DateTime.Parse("2019-06-28"),
                    PurchaseDate = DateTime.Parse("2023-04-07"),
                    TotalTimePlayed = TimeSpan.FromHours(0),
                    Url = "https://store.playstation.com/sv-se/product/EP9000-CUSA10211_00-HRZCE00000000000",
                    CreatedBy = "System",
                    CreatedAt = DateTime.Now,
                    UpdatedBy = null,
                    UpdatedAt = null,
                    DeletedBy = null,
                    DeletedAt = null
                },
                new VideoGame
                {
                    Id = 2,
                    Name = "Horizon Forbidden West - Complete Edition",
                    Title = "Horizon Forbidden West - Complete Edition",
                    ImageUri = null,
                    DeveloperId = 3,
                    Price = 799.0M,
                    ReleaseDate = DateTime.Parse("2023-10-06"),
                    PurchaseDate = DateTime.Parse("2023-11-24"),
                    TotalTimePlayed = TimeSpan.FromHours(0),
                    Url = "https://www.playstation.com/sv-se/games/horizon-forbidden-west/",
                    CreatedBy = "System",
                    CreatedAt = DateTime.Now,
                    UpdatedBy = null,
                    UpdatedAt = null,
                    DeletedBy = null,
                    DeletedAt = null
                },
                new VideoGame
                {
                    Id = 3,
                    Name = "Horizon Call of the Mountain",
                    Title = "Horizon Call of the Mountain",
                    ImageUri = null,
                    DeveloperId = 3,
                    Price = 739.0M,
                    ReleaseDate = DateTime.Parse("2023-02-22"),
                    PurchaseDate = DateTime.Parse("2023-02-25"),
                    TotalTimePlayed = TimeSpan.FromHours(0),
                    Url = "https://www.playstation.com/sv-se/games/horizon-call-of-the-mountain/",
                    CreatedBy = "System",
                    CreatedAt = DateTime.Now,
                    UpdatedBy = null,
                    UpdatedAt = null,
                    DeletedBy = null,
                    DeletedAt = null
                },
                new VideoGame
                {
                    Id = 4,
                    Name = "Ghost of Tsushima DIRECTOR’S CUT",
                    Title = "Ghost of Tsushima DIRECTOR’S CUT",
                    ImageUri = null,
                    DeveloperId = 4,
                    Price = 0.0M,
                    ReleaseDate = DateTime.Parse("2021-08-20"),
                    PurchaseDate = DateTime.Parse("2022-05-03"),
                    TotalTimePlayed = TimeSpan.FromHours(0),
                    Url = "https://www.playstation.com/en-se/games/ghost-of-tsushima/",
                    CreatedBy = "System",
                    CreatedAt = DateTime.Now,
                    UpdatedBy = null,
                    UpdatedAt = null,
                    DeletedBy = null,
                    DeletedAt = null
                }
            );
        }
    }
}
