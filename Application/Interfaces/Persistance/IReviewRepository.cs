using GameStore.Domain.Dtos;
using GameStore.Domain.Entities;
using GameStore.Domain.Filters;

namespace GameStore.Application.Interfaces.Persistance
{
    public interface IReviewRepository : IRepositoryBase<Review, ReviewDto, ReviewFilter>
    {
        Task<IEnumerable<ReviewDto>> ReadByGradeAsync(int grade, CancellationToken cancellationToken);

        Task<IEnumerable<ReviewDto>> ReadByGradeAsync(int fromGrade, int toGrade, CancellationToken cancellationToken);

        Task<IEnumerable<ReviewDto>> ReadByReviewDateAsync(DateTime fromReviewDate, DateTime toReviewDate, CancellationToken cancellationToken);

        Task<IEnumerable<ReviewDto>> ReadByVideoGameIdAsync(int videoGameId, CancellationToken cancellationToken);
    }
}
