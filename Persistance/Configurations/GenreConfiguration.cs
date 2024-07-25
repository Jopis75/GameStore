using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Configurations
{
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasData(
                new Genre
                {
                    Id = 1,
                    Name = "Action",
                    Description = null
                },
                new Genre
                {
                    Id = 2,
                    Name = "Adventure",
                    Description = null
                },
                new Genre
                {
                    Id = 3,
                    Name = "Puzzle",
                    Description = null
                },
                new Genre
                {
                    Id = 4,
                    Name = "Casual",
                    Description = null
                },
                new Genre
                {
                    Id = 5,
                    Name = "Role Playing Games",
                    Description = null
                },
                new Genre
                {
                    Id = 6,
                    Name = "Arcade",
                    Description = null
                },
                new Genre
                {
                    Id = 7,
                    Name = "Shooter",
                    Description = null
                },
                new Genre
                {
                    Id = 8,
                    Name = "Simulation",
                    Description = null
                },
                new Genre
                {
                    Id = 9,
                    Name = "Strategy",
                    Description = null
                },
                new Genre
                {
                    Id = 10,
                    Name = "Horror",
                    Description = null
                },
                new Genre
                {
                    Id = 11,
                    Name = "Driving/Racing",
                    Description = null
                },
                new Genre
                {
                    Id = 12,
                    Name = "Unique",
                    Description = null
                },
                new Genre
                {
                    Id = 13,
                    Name = "Sport",
                    Description = null
                },
                new Genre
                {
                    Id = 14,
                    Name = "Family",
                    Description = null
                },
                new Genre
                {
                    Id = 15,
                    Name = "Fighting",
                    Description = null
                },
                new Genre
                {
                    Id = 16,
                    Name = "Party",
                    Description = null
                },
                new Genre
                {
                    Id = 17,
                    Name = "Simulator",
                    Description = null
                },
                new Genre
                {
                    Id = 18,
                    Name = "Music/Rythm",
                    Description = null
                },
                new Genre
                {
                    Id = 19,
                    Name = "Adult",
                    Description = null
                },
                new Genre
                {
                    Id = 20,
                    Name = "Brain Training",
                    Description = null
                },
                new Genre
                {
                    Id = 21,
                    Name = "Educational",
                    Description = null
                },
                new Genre
                {
                    Id = 22,
                    Name = "Fitness",
                    Description = null
                },
                new Genre
                {
                    Id = 23,
                    Name = "Quiz",
                    Description = null
                }
            );
        }
    }
}
