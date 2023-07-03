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
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(
                new Product
                {
                    Id = 1,
                    Title = "Horizon Forbidden West",                    
                    Price = 649,
                    ReleaseDate = DateTime.Parse("2022-02-18"),
                    PurchaseDate = DateTime.Parse("2022-07-18"),
                    TotalTimePlayed = TimeSpan.FromHours(0),
                    Url = "https://www.playstation.com/sv-se/games/horizon-forbidden-west/",
                    CoverImageUri = string.Empty,
                    CreatedBy = "System",
                    CreatedAt = DateTime.Now,
                    UpdatedBy = string.Empty,
                    UpdatedAt = null,
                    DeletedBy = string.Empty,
                    DeletedAt = null
                },
                new Product
                {
                    Id = 2,
                    Title = "Horizon Call of the Mountain",
                    DeveloperId = 2,
                    Price = 739,
                    ReleaseDate = DateTime.Parse("2023-02-22"),
                    PurchaseDate = DateTime.Parse("2023-02-24"),
                    TotalTimePlayed = TimeSpan.FromHours(0),
                    Url = "https://www.playstation.com/en-se/games/horizon-call-of-the-mountain/",
                    CoverImageUri = string.Empty,
                    CreatedBy = "System",
                    CreatedAt = DateTime.Now,
                    UpdatedBy = string.Empty,
                    UpdatedAt = null,
                    DeletedBy = string.Empty,
                    DeletedAt = null
                },
                new Product
                {
                    Id = 3,
                    Title = "Ghost of Tsushima",
                    DeveloperId = 3,
                    Price = 699,
                    ReleaseDate = DateTime.Parse("2020-07-17"),
                    PurchaseDate = DateTime.Parse("2022-05-03"),
                    TotalTimePlayed = TimeSpan.FromHours(0),
                    Url = "https://www.playstation.com/en-se/games/ghost-of-tsushima/",
                    CoverImageUri = string.Empty,
                    CreatedBy = "System",
                    CreatedAt = DateTime.Now,
                    UpdatedBy = string.Empty,
                    UpdatedAt = null,
                    DeletedBy = string.Empty,
                    DeletedAt = null
                }
            );
        }
    }
}
