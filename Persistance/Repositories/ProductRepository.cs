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

        public async Task<IEnumerable<Product>> GetByDeveloperIdAsync(int videoGameDeveloperId, bool asNoTracking = false)
        {
            var products = asNoTracking ?
                await Entities
                    .AsNoTracking<Product>()
                    .Where(entity => entity.DeveloperId == videoGameDeveloperId)
                    .ToListAsync() :
                await Entities
                    .Where(entity => entity.DeveloperId == videoGameDeveloperId)
                    .ToListAsync();

            return products;
        }

        public async Task<IEnumerable<Product>> GetByPriceAsync(decimal fromPrice, decimal toPrice, bool asNoTracking = false)
        {
            var products = asNoTracking ?
                await Entities
                    .AsNoTracking<Product>()
                    .Where(entity => entity.Price >= fromPrice && entity.Price <= toPrice)
                    .ToListAsync() :
                await Entities
                    .Where(entity => entity.Price >= fromPrice && entity.Price <= toPrice)
                    .ToListAsync();

            return products;
        }

        public async Task<IEnumerable<Product>> GetByPurchaseDateAsync(DateTime fromPurchaseDate, DateTime toPurchaseDate, bool asNoTracking = false)
        {
            var products = asNoTracking ?
                await Entities
                    .AsNoTracking<Product>()
                    .Where(entity => entity.PurchaseDate >= fromPurchaseDate && entity.PurchaseDate <= toPurchaseDate)
                    .ToListAsync() :
                await Entities
                    .Where(entity => entity.PurchaseDate >= fromPurchaseDate && entity.PurchaseDate <= toPurchaseDate)
                    .ToListAsync();

            return products;
        }

        public async Task<IEnumerable<Product>> GetByReleaseDateAsync(DateTime fromReleaseDate, DateTime toReleaseDate, bool asNoTracking = false)
        {
            var products = asNoTracking ?
                await Entities
                    .AsNoTracking<Product>()
                    .Where(entity => entity.ReleaseDate >= fromReleaseDate && entity.ReleaseDate <= toReleaseDate)
                    .ToListAsync() :
                await Entities
                    .Where(entity => entity.ReleaseDate >= fromReleaseDate && entity.ReleaseDate <= toReleaseDate)
                    .ToListAsync();

            return products;
        }

        public async Task<Product> GetByTitleAsync(string title, bool asNoTracking = false)
        {
            var product = asNoTracking ?
                await Entities
                    .AsNoTracking<Product>()
                    .Where(entity => entity.Title == title)
                    .SingleOrDefaultAsync() :
                await Entities
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
