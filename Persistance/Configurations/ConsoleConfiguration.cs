using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Console = Domain.Entities.Console;

namespace Persistance.Configurations
{
    public class ConsoleConfiguration : IEntityTypeConfiguration<Console>
    {
        public void Configure(EntityTypeBuilder<Console> builder)
        {
            builder.HasData(
                new Console
                {
                    Id = 1,
                    Name = "PlayStation 5",
                    DeveloperId = 1,
                    Price = 9988.0M,
                    ReleaseDate = DateTime.Parse("2020-11-12"),
                    PurchaseDate = DateTime.Parse("2022-01-29"),
                    Url = "https://www.playstation.com/sv-se/ps5/",
                    ImageUri = null,
                    CreatedBy = "System",
                    CreatedAt = DateTime.Now,
                    UpdatedBy = null,
                    UpdatedAt = null,
                    DeletedBy = null,
                    DeletedAt = null
                },
                new Console
                {
                    Id = 2,
                    Name = "PlayStation VR2",
                    DeveloperId = 1,
                    Price = 7869.0M,
                    ReleaseDate = DateTime.Parse("2023-02-22"),
                    PurchaseDate = DateTime.Parse("2023-02-22"),
                    Url = "https://www.playstation.com/sv-se/ps-vr2/",
                    ImageUri = null,
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
