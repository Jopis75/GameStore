using Application.Interfaces.Persistance;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistance.DbContexts;

namespace Persistance.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(GameStoreDbContext gameStoreDbContext)
            : base(gameStoreDbContext) { }

        public async Task<IEnumerable<Product>> GetByDeveloperIdAsync(int videoGameDeveloperId)
        {
            var products = await Entities
                .Where(entity => entity.VideoGameDeveloperId == videoGameDeveloperId)
                .ToListAsync();

            return products;
        }

        public async Task<IEnumerable<Product>> GetByPriceAsync(decimal fromPrice, decimal toPrice)
        {
            var products = await Entities
                .Where(entity => entity.Price >= fromPrice && entity.Price <= toPrice)
                .ToListAsync();

            return products;
        }

        public async Task<IEnumerable<Product>> GetByPurchaseDateAsync(DateTime fromPurchaseDate, DateTime toPurchaseDate)
        {
            var products = await Entities
                .Where(entity => entity.PurchaseDate >= fromPurchaseDate && entity.PurchaseDate <= toPurchaseDate)
                .ToListAsync();

            return products;
        }

        public async Task<IEnumerable<Product>> GetByReleaseDateAsync(DateTime fromReleaseDate, DateTime toReleaseDate)
        {
            var products = await Entities
                .Where(entity => entity.ReleaseDate >= fromReleaseDate && entity.ReleaseDate <= toReleaseDate)
                .ToListAsync();

            return products;
        }

        public async Task<Product> GetByTitleAsync(string title)
        {
            var product = await Entities
                .Where(entity => entity.Title == title)
                .SingleOrDefaultAsync();

            if (product == null)
            {
                return new Product();
            }

            return product;
        }
    }
}
