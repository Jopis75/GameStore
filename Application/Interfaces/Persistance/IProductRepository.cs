using Domain.Entities;

namespace Application.Interfaces.Persistance
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        Task<IEnumerable<Product>> ReadByDeveloperIdAsync(int videoGameDeveloperId, bool asNoTracking = false);

        Task<IEnumerable<Product>> ReadByPriceAsync(decimal fromPrice, decimal toPrice, bool asNoTracking = false);

        Task<IEnumerable<Product>> ReadByPurchaseDateAsync(DateTime fromPurchaseDate, DateTime toPurchaseDate, bool asNoTracking = false);

        Task<IEnumerable<Product>> ReadByReleaseDateAsync(DateTime fromReleaseDate, DateTime toReleaseDate, bool asNoTracking = false);

        Task<Product> ReadByTitleAsync(string title, bool asNoTracking = false);
    }
}
