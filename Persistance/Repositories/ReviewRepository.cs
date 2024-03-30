using Application.Interfaces.Persistance;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistance.DbContexts;

namespace Persistance.Repositories
{
    public class ReviewRepository : RepositoryBase<Review>, IReviewRepository
    {
        public ReviewRepository(GameStoreDbContext gameStoreDbContext)
            : base(gameStoreDbContext)
        { }

        public async Task<IEnumerable<Review>> ReadByGradeAsync(int grade, bool asNoTracking = false)
        {
            var reviews = asNoTracking ?
                await Entities
                    .AsNoTracking<Review>()
                    .Where(review => review.Grade == grade)
                    .ToListAsync() :
                await Entities
                    .Where(review => review.Grade == grade)
                    .ToListAsync();

            return reviews;
        }

        public async Task<IEnumerable<Review>> ReadByGradeAsync(int fromGrade, int toGrade, bool asNoTracking = false)
        {
            var reviews = asNoTracking ?
                await Entities
                    .AsNoTracking<Review>()
                    .Where(review => review.Grade >= fromGrade && review.Grade <= toGrade)
                    .ToListAsync() :
                await Entities
                    .Where(review => review.Grade >= fromGrade && review.Grade <= toGrade)
                    .ToListAsync();

            return reviews;
        }

        public async Task<IEnumerable<Review>> ReadByReviewDateAsync(DateTime fromReviewDate, DateTime toReviewDate, bool asNoTracking = false)
        {
            var reviews = asNoTracking ?
                await Entities
                    .AsNoTracking<Review>()
                    .Where(review => review.ReviewDate >= fromReviewDate && review.ReviewDate <= toReviewDate)
                    .ToListAsync() :
                await Entities
                    .Where(review => review.ReviewDate >= fromReviewDate && review.ReviewDate <= toReviewDate)
                    .ToListAsync();

            return reviews;
        }

        public async Task<IEnumerable<Review>> ReadByVideoGameIdAsync(int videoGameId, bool asNoTracking = false)
        {
            var reviews = asNoTracking ?
                await Entities
                    .AsNoTracking<Review>()
                    .Where(review => review.VideoGameId == videoGameId)
                    .ToListAsync() :
                await Entities
                    .Where(review => review.VideoGameId == videoGameId)
                    .ToListAsync();

            return reviews;
        }
    }
}
