using Domain.Dtos;
using Domain.Entities;
using Domain.Filters;

namespace Application.Interfaces.Persistance
{
    public interface IReviewRepository : IRepositoryBase<Review, ReviewDto, ReviewFilter>
    {
        Task<IEnumerable<ReviewDto>> ReadByGradeAsync(int grade, bool asNoTracking = false);

        Task<IEnumerable<ReviewDto>> ReadByGradeAsync(int fromGrade, int toGrade, bool asNoTracking = false);

        Task<IEnumerable<ReviewDto>> ReadByReviewDateAsync(DateTime fromReviewDate, DateTime toReviewDate, bool asNoTracking = false);

        Task<IEnumerable<ReviewDto>> ReadByVideoGameIdAsync(int videoGameId, bool asNoTracking = false);
    }
}
