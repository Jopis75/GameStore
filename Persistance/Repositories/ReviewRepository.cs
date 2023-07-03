using Application.Interfaces.Persistance;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistance.DbContexts;

namespace Persistance.Repositories
{
    public class ReviewRepository : RepositoryBase<Review>, IReviewRepository
    {
        public ReviewRepository(GameStoreDbContext gameStoreDbContext)
            : base(gameStoreDbContext) { }

        public async Task<IEnumerable<Review>> GetByGradeAsync(int grade, bool asNoTracking = false)
        {
            var reviews = asNoTracking ?
                await Entities
                    .AsNoTracking<Review>()
                    .Where(entity => entity.Grade == grade)
                    .ToListAsync() :
                await Entities
                    .Where(entity => entity.Grade == grade)
                    .ToListAsync();

            return reviews;
        }

        public async Task<IEnumerable<Review>> GetByGradeAsync(int fromGrade, int toGrade, bool asNoTracking = false)
        {
            var reviews = asNoTracking ?
                await Entities
                    .AsNoTracking<Review>()
                    .Where(entity => entity.Grade >= fromGrade && entity.Grade <= toGrade)
                    .ToListAsync() :
                await Entities
                    .Where(entity => entity.Grade >= fromGrade && entity.Grade <= toGrade)
                    .ToListAsync();

            return reviews;
        }

        public async Task<Review> GetByProductIdAsync(int videoGameId, bool asNoTracking = false)
        {
            var review = asNoTracking ?
                await Entities
                    .AsNoTracking<Review>()
                    .Where(entity => entity.VideoGameId == videoGameId)
                    .SingleOrDefaultAsync() :
                await Entities
                    .Where(entity => entity.VideoGameId == videoGameId)
                    .SingleOrDefaultAsync();

            if (review == null)
            {
                return new Review();
            }

            return review;
        }

        public async Task<IEnumerable<Review>> GetByReviewDateAsync(DateTime fromReviewDate, DateTime toReviewDate, bool asNoTracking = false)
        {
            var reviews = asNoTracking ?
                await Entities
                    .AsNoTracking<Review>()
                    .Where(entity => entity.ReviewDate >= fromReviewDate && entity.ReviewDate <= toReviewDate)
                    .ToListAsync() :
                await Entities
                    .Where(entity => entity.ReviewDate >= fromReviewDate && entity.ReviewDate <= toReviewDate)
                    .ToListAsync();

            return reviews;
        }
    }
}
