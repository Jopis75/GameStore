using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Configurations
{
    public class ConsoleProductConfiguration : IEntityTypeConfiguration<ConsoleVideoGame>
    {
        public void Configure(EntityTypeBuilder<ConsoleVideoGame> builder)
        {
            builder.HasData(
                new ConsoleVideoGame
                {
                    ConsoleId = 1,
                    VideoGameId = 1
                },
                new ConsoleVideoGame
                {
                    ConsoleId = 1,
                    VideoGameId = 2
                },
                new ConsoleVideoGame
                {
                    ConsoleId = 1,
                    VideoGameId = 3
                }
            );
        }
    }
}
