using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Configurations
{
    public class VideoGameGenreConfiguration : IEntityTypeConfiguration<VideoGameGenre>
    {
        public void Configure(EntityTypeBuilder<VideoGameGenre> builder)
        {
            builder.HasData(
                new VideoGameGenre
                {
                    Id = 1,
                    VideoGameId = 1,
                    GenreId = 1
                },
                new VideoGameGenre
                {
                    Id = 2,
                    VideoGameId = 1,
                    GenreId = 5
                },
                new VideoGameGenre
                {
                    Id = 3,
                    VideoGameId = 2,
                    GenreId = 1
                },
                new VideoGameGenre
                {
                    Id = 4,
                    VideoGameId = 2,
                    GenreId = 5
                },
                new VideoGameGenre
                {
                    Id = 5,
                    VideoGameId = 3,
                    GenreId = 1
                },
                new VideoGameGenre
                {
                    Id = 6,
                    VideoGameId = 3,
                    GenreId = 2
                },
                new VideoGameGenre
                {
                    Id = 7,
                    VideoGameId = 4,
                    GenreId = 1
                },
                new VideoGameGenre
                {
                    Id = 8,
                    VideoGameId = 4,
                    GenreId = 5
                }
            );
        }
    }
}
