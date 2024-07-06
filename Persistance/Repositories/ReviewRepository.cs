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
    public class ReviewRepository : RepositoryBase<Review, ReviewDto, ReviewFilter>, IReviewRepository
    {
        public ReviewRepository(GameStoreDbContext gameStoreDbContext, IMapper mapper)
            : base(gameStoreDbContext, mapper)
        {
        }

        protected override async Task<IEnumerable<ReviewDto>> ReadByFilterAsync(ReviewFilter filter, Expression<Func<Review, bool>> predicate, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (filter.Grade != null)
            {
                predicate = predicate.And(review => review.Grade == filter.Grade);
            }

            if (filter.ReviewDate != null)
            {
                predicate = predicate.And(review => review.ReviewDate.Date == filter.ReviewDate.Value.Date);
            }

            var reviews = await Entities
                .AsNoTracking()
                .Where(predicate)
                .ToArrayAsync();

            return reviews.Select(Mapper.Map<ReviewDto>);
        }

        public async Task<IEnumerable<ReviewDto>> ReadByGradeAsync(int grade, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var reviews = await Entities
                .AsNoTracking()
                .Where(review => review.Grade == grade)
                .ToArrayAsync();

            return reviews.Select(Mapper.Map<ReviewDto>);
        }

        public async Task<IEnumerable<ReviewDto>> ReadByGradeAsync(int fromGrade, int toGrade, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var reviews = await Entities
                .AsNoTracking()
                .Where(review => review.Grade >= fromGrade && review.Grade <= toGrade)
                .ToArrayAsync();

            return reviews.Select(Mapper.Map<ReviewDto>);
        }

        public async Task<IEnumerable<ReviewDto>> ReadByReviewDateAsync(DateTime fromReviewDate, DateTime toReviewDate, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var reviews = await Entities
                .AsNoTracking()
                .Where(review => review.ReviewDate.Date >= fromReviewDate.Date && review.ReviewDate.Date <= toReviewDate.Date)
                .ToArrayAsync();

            return reviews.Select(Mapper.Map<ReviewDto>);
        }

        public async Task<IEnumerable<ReviewDto>> ReadByVideoGameIdAsync(int videoGameId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var reviews = await Entities
                .AsNoTracking()
                .Where(review => review.VideoGameId == videoGameId)
                .ToArrayAsync();

            return reviews.Select(Mapper.Map<ReviewDto>);
        }
    }
}
