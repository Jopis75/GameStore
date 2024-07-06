using Domain.Dtos;
using Domain.Entities;
using Domain.Filters;

namespace Application.Interfaces.Persistance
{
    public interface IReviewRepository : IRepositoryBase<Review, ReviewDto, ReviewFilter>
    {
        Task<IEnumerable<ReviewDto>> ReadByGradeAsync(int grade, CancellationToken cancellationToken);

        Task<IEnumerable<ReviewDto>> ReadByGradeAsync(int fromGrade, int toGrade, CancellationToken cancellationToken);

        Task<IEnumerable<ReviewDto>> ReadByReviewDateAsync(DateTime fromReviewDate, DateTime toReviewDate, CancellationToken cancellationToken);

        Task<IEnumerable<ReviewDto>> ReadByVideoGameIdAsync(int videoGameId, CancellationToken cancellationToken);
    }
}
