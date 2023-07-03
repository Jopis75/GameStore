using Domain.Entities;

namespace Application.Interfaces.Persistance
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        Task<IEnumerable<Product>> GetByDeveloperIdAsync(int videoGameDeveloperId, bool asNoTracking = false);

        Task<IEnumerable<Product>> GetByPriceAsync(decimal fromPrice, decimal toPrice, bool asNoTracking = false);

        Task<IEnumerable<Product>> GetByPurchaseDateAsync(DateTime fromPurchaseDate, DateTime toPurchaseDate, bool asNoTracking = false);

        Task<IEnumerable<Product>> GetByReleaseDateAsync(DateTime fromReleaseDate, DateTime toReleaseDate, bool asNoTracking = false);

        Task<Product> GetByTitleAsync(string title, bool asNoTracking = false);
    }
}
