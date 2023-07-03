using Domain.Entities;

namespace Application.Interfaces.Persistance
{
    public interface IReviewRepository : IRepositoryBase<Review>
    {
        Task<IEnumerable<Review>> GetByGradeAsync(int grade, bool asNoTracking = false);

        Task<IEnumerable<Review>> GetByGradeAsync(int fromGrade, int toGrade, bool asNoTracking = false);

        Task<Review> GetByProductIdAsync(int videoGameId, bool asNoTracking = false);

        Task<IEnumerable<Review>> GetByReviewDateAsync(DateTime fromReviewDate, DateTime toReviewDate, bool asNoTracking = false);
    }
}
