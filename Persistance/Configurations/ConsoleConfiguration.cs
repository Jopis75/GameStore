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
                    Name = "PlayStation®5 Console",
                    DeveloperId = 1,
                    Price = 9988.00M,
                    ReleaseDate = DateTime.Parse("2020-11-12"),
                    PurchaseDate = DateTime.Parse("2022-01-29"),
                    Url = "https://direct.playstation.com/en-us/buy-consoles/playstation5-console/",
                    ImageUri = string.Empty,
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
