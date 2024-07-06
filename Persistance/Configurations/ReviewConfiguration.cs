using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistance.Configurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasData(
                new Review
                {
                    Id = 1,
                    ConsoleId = 1,
                    ReviewDate = DateTime.Now,
                    ReviewText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                    Grade = 100,
                    CreatedBy = "System",
                    CreatedAt = DateTime.Now,
                    UpdatedBy = String.Empty,
                    UpdatedAt = null,
                    DeletedBy = String.Empty,
                    DeletedAt = null
                },
                new Review
                {
                    Id = 2,
                    VideoGameId = 1,
                    ReviewDate = DateTime.Now,
                    ReviewText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                    Grade = 100,
                    CreatedBy = "System",
                    CreatedAt = DateTime.Now,
                    UpdatedBy = String.Empty,
                    UpdatedAt = null,
                    DeletedBy = String.Empty,
                    DeletedAt = null
                },
                new Review
                {
                    Id = 3,
                    VideoGameId = 1,
                    ReviewDate = DateTime.Now,
                    ReviewText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                    Grade = 100,
                    CreatedBy = "System",
                    CreatedAt = DateTime.Now,
                    UpdatedBy = String.Empty,
                    UpdatedAt = null,
                    DeletedBy = String.Empty,
                    DeletedAt = null
                },
                new Review
                {
                    Id = 4,
                    VideoGameId = 1,
                    ReviewDate = DateTime.Now,
                    ReviewText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                    Grade = 100,
                    CreatedBy = "System",
                    CreatedAt = DateTime.Now,
                    UpdatedBy = String.Empty,
                    UpdatedAt = null,
                    DeletedBy = String.Empty,
                    DeletedAt = null
                },
                new Review
                {
                    Id = 5,
                    VideoGameId = 3,
                    ReviewDate = DateTime.Now,
                    ReviewText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                    Grade = 100,
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
