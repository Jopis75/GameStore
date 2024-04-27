using Application.Interfaces.Persistance;
using Domain.Entities;
using Domain.Filters;
using Microsoft.EntityFrameworkCore;
using Persistance.DbContexts;

namespace Persistance.Repositories
{
    public class ReviewRepository : RepositoryBase<Review, ReviewFilter>, IReviewRepository
    {
        public ReviewRepository(GameStoreDbContext gameStoreDbContext)
            : base(gameStoreDbContext) { }

        public override Task<IEnumerable<Review>> ReadByFilterAsync(ReviewFilter filter, bool asNoTracking = false)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Review>> ReadByGradeAsync(int grade, bool asNoTracking = false)
        {
            var query = Entities.AsQueryable();

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            var reviews = await query
                .Where(review => review.Grade == grade)
                .ToListAsync();

            return reviews;
        }

        public async Task<IEnumerable<Review>> ReadByGradeAsync(int fromGrade, int toGrade, bool asNoTracking = false)
        {
            var query = Entities.AsQueryable();

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            var reviews = await query
                .Where(review => review.Grade >= fromGrade && review.Grade <= toGrade)
                .ToListAsync();

            return reviews;
        }

        public async Task<IEnumerable<Review>> ReadByReviewDateAsync(DateTime fromReviewDate, DateTime toReviewDate, bool asNoTracking = false)
        {
            var query = Entities.AsQueryable();

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            var reviews = await query
                .Where(review => review.ReviewDate.Date >= fromReviewDate.Date && review.ReviewDate.Date <= toReviewDate.Date)
                .ToListAsync();

            return reviews;
        }

        public async Task<IEnumerable<Review>> ReadByVideoGameIdAsync(int videoGameId, bool asNoTracking = false)
        {
            var query = Entities.AsQueryable();

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            var reviews = await query
                .Where(review => review.VideoGameId == videoGameId)
                .ToListAsync();

            return reviews;
        }
    }
}
