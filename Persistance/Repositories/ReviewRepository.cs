using Abp.Linq.Expressions;
using Application.Interfaces.Persistance;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Filters;
using Microsoft.EntityFrameworkCore;
using Persistance.DbContexts;
using System.Linq.Expressions;

namespace Persistance.Repositories
{
    public class ReviewRepository(GameStoreDbContextFactory gameStoreDbContextFactory, IMapper mapper) : RepositoryBase<Review, ReviewDto, ReviewFilter>(gameStoreDbContextFactory, mapper), IReviewRepository
    {
        protected override async Task<IEnumerable<ReviewDto>> ReadByFilterAsync(ReviewFilter filter, Expression<Func<Review, bool>> predicate, CancellationToken cancellationToken)
        {
            if (filter.Grade != null)
            {
                predicate = predicate.And(review => review.Grade == filter.Grade);
            }

            if (filter.ReviewDate != null)
            {
                predicate = predicate.And(review => review.ReviewDate.Date == filter.ReviewDate.Value.Date);
            }

            using var gameStoreDbContext = gameStoreDbContextFactory.CreateGameStoreDbContext();

            var reviews = await gameStoreDbContext
                .Set<Review>()
                .AsNoTracking()
                .Where(predicate)
                .ToArrayAsync(cancellationToken);

            return reviews.Select(mapper.Map<ReviewDto>);
        }

        public async Task<IEnumerable<ReviewDto>> ReadByGradeAsync(int grade, CancellationToken cancellationToken)
        {
            using var gameStoreDbContext = gameStoreDbContextFactory.CreateGameStoreDbContext();

            var reviews = await gameStoreDbContext
                .Set<Review>()
                .AsNoTracking()
                .Where(review => review.Grade == grade)
                .ToArrayAsync(cancellationToken);

            return reviews.Select(mapper.Map<ReviewDto>);
        }

        public async Task<IEnumerable<ReviewDto>> ReadByGradeAsync(int fromGrade, int toGrade, CancellationToken cancellationToken)
        {
            using var gameStoreDbContext = gameStoreDbContextFactory.CreateGameStoreDbContext();

            var reviews = await gameStoreDbContext
                .Set<Review>()
                .AsNoTracking()
                .Where(review => review.Grade >= fromGrade && review.Grade <= toGrade)
                .ToArrayAsync(cancellationToken);

            return reviews.Select(mapper.Map<ReviewDto>);
        }

        public async Task<IEnumerable<ReviewDto>> ReadByReviewDateAsync(DateTime fromReviewDate, DateTime toReviewDate, CancellationToken cancellationToken)
        {
            using var gameStoreDbContext = gameStoreDbContextFactory.CreateGameStoreDbContext();

            var reviews = await gameStoreDbContext
                .Set<Review>()
                .AsNoTracking()
                .Where(review => review.ReviewDate.Date >= fromReviewDate.Date && review.ReviewDate.Date <= toReviewDate.Date)
                .ToArrayAsync(cancellationToken);

            return reviews.Select(mapper.Map<ReviewDto>);
        }

        public async Task<IEnumerable<ReviewDto>> ReadByVideoGameIdAsync(int videoGameId, CancellationToken cancellationToken)
        {
            using var gameStoreDbContext = gameStoreDbContextFactory.CreateGameStoreDbContext();

            var reviews = await gameStoreDbContext
                .Set<Review>()
                .AsNoTracking()
                .Where(review => review.VideoGameId == videoGameId)
                .ToArrayAsync(cancellationToken);

            return reviews.Select(mapper.Map<ReviewDto>);
        }
    }
}
