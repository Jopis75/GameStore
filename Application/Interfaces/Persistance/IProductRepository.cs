using Domain.Entities;

namespace Application.Interfaces.Persistance
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        Task<IEnumerable<Product>> GetByDeveloperIdAsync(int videoGameDeveloperId);

        Task<IEnumerable<Product>> GetByPriceAsync(decimal fromPrice, decimal toPrice);

        Task<IEnumerable<Product>> GetByPurchaseDateAsync(DateTime fromPurchaseDate, DateTime toPurchaseDate);

        Task<IEnumerable<Product>> GetByReleaseDateAsync(DateTime fromReleaseDate, DateTime toReleaseDate);

        Task<Product> GetByTitleAsync(string title);
    }
}
