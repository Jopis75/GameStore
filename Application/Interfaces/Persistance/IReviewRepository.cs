using Domain.Entities;
using Domain.Filters;

namespace Application.Interfaces.Persistance
{
    public interface IReviewRepository : IRepositoryBase<Review, ReviewFilter>
    {
        Task<IEnumerable<Review>> ReadByGradeAsync(int grade, bool asNoTracking = false);

        Task<IEnumerable<Review>> ReadByGradeAsync(int fromGrade, int toGrade, bool asNoTracking = false);

        Task<IEnumerable<Review>> ReadByReviewDateAsync(DateTime fromReviewDate, DateTime toReviewDate, bool asNoTracking = false);

        Task<IEnumerable<Review>> ReadByVideoGameIdAsync(int videoGameId, bool asNoTracking = false);
    }
}
