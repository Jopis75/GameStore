using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

// 2022-05-03, 699
// 2022-07-07, 995
// 2022-07-18, 649
// 2022-08-25, 549
// 2022-12-30, 728

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
                    Name = "Horizon Forbidden West",
                    Title = "Horizon Forbidden West",
                    ImageUri = String.Empty,
                    DeveloperId = 2,
                    Price = 69.99M,
                    ReleaseDate = DateTime.Parse("2022-02-18"),
                    PurchaseDate = DateTime.Parse("2022-07-18"),
                    TotalTimePlayed = TimeSpan.FromHours(0),
                    Url = "https://www.playstation.com/sv-se/games/horizon-forbidden-west/",
                    CreatedBy = "System",
                    CreatedAt = DateTime.Now,
                    UpdatedBy = String.Empty,
                    UpdatedAt = null,
                    DeletedBy = String.Empty,
                    DeletedAt = null
                },
                new VideoGame
                {
                    Id = 2,
                    Name = "Horizon Call of the Mountain",
                    Title = "Horizon Call of the Mountain",
                    ImageUri = String.Empty,
                    DeveloperId = 2,
                    Price = 59.99M,
                    ReleaseDate = DateTime.Parse("2023-02-22"),
                    PurchaseDate = DateTime.Parse("2023-02-24"),
                    TotalTimePlayed = TimeSpan.FromHours(0),
                    Url = "https://www.playstation.com/en-se/games/horizon-call-of-the-mountain/",
                    CreatedBy = "System",
                    CreatedAt = DateTime.Now,
                    UpdatedBy = String.Empty,
                    UpdatedAt = null,
                    DeletedBy = String.Empty,
                    DeletedAt = null
                },
                new VideoGame
                {
                    Id = 3,
                    Name = "Ghost of Tsushima DIRECTOR’S CUT",
                    Title = "Ghost of Tsushima DIRECTOR’S CUT",
                    ImageUri = String.Empty,
                    DeveloperId = 3,
                    Price = 69.99M,
                    ReleaseDate = DateTime.Parse("2020-07-17"),
                    PurchaseDate = DateTime.Parse("2022-05-03"),
                    TotalTimePlayed = TimeSpan.FromHours(0),
                    Url = "https://www.playstation.com/en-se/games/ghost-of-tsushima/",
                    CreatedBy = "System",
                    CreatedAt = DateTime.Now,
                    UpdatedBy = String.Empty,
                    UpdatedAt = null,
                    DeletedBy = String.Empty,
                    DeletedAt = null
                }
            );
        }
    }
}
