using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Configurations
{
    public class ConsoleVideoGameConfiguration : IEntityTypeConfiguration<ConsoleVideoGame>
    {
        public void Configure(EntityTypeBuilder<ConsoleVideoGame> builder)
        {
            builder.HasData(
                new ConsoleVideoGame
                {
                    Id = 1,
                    ConsoleId = 1,
                    VideoGameId = 1
                },
                new ConsoleVideoGame
                {
                    Id = 2,
                    ConsoleId = 1,
                    VideoGameId = 2
                },
                new ConsoleVideoGame
                {
                    Id = 3,
                    ConsoleId = 1,
                    VideoGameId = 3
                }
            );
        }
    }
}
