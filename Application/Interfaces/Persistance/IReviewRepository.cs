using Domain.Entities;

namespace Application.Interfaces.Persistance
{
    public interface IReviewRepository : IRepositoryBase<Review>
    {
        Task<IEnumerable<Review>> GetByGradeAsync(int grade);

        Task<IEnumerable<Review>> GetByGradeAsync(int fromGrade, int toGrade);

        Task<Review> GetByProductIdAsync(int videoGameId);

        Task<IEnumerable<Review>> GetByReviewDateAsync(DateTime fromReviewDate, DateTime toReviewDate);
    }
}
