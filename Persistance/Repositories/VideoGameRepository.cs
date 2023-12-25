using Application.Interfaces.Persistance;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistance.DbContexts;

namespace Persistance.Repositories
{
    public class VideoGameRepository : RepositoryBase<VideoGame>, IVideoGameRepository
    {
        public VideoGameRepository(GameStoreDbContext gameStoreDbContext)
            : base(gameStoreDbContext) { }

        public async Task<IEnumerable<VideoGame>> ReadByDeveloperIdAsync(int developerId, bool asNoTracking = false)
        {
            var products = asNoTracking ?
                await Entities
                    .AsNoTracking<VideoGame>()
                    .Where(entity => entity.DeveloperId == developerId)
                    .ToListAsync() :
                await Entities
                    .Where(entity => entity.DeveloperId == developerId)
                    .ToListAsync();

            return products;
        }

        public async Task<IEnumerable<VideoGame>> ReadByPriceAsync(decimal fromPrice, decimal toPrice, bool asNoTracking = false)
        {
            var products = asNoTracking ?
                await Entities
                    .AsNoTracking<VideoGame>()
                    .Where(entity => entity.Price >= fromPrice && entity.Price <= toPrice)
                    .ToListAsync() :
                await Entities
                    .Where(entity => entity.Price >= fromPrice && entity.Price <= toPrice)
                    .ToListAsync();

            return products;
        }

        public async Task<IEnumerable<VideoGame>> ReadByPurchaseDateAsync(DateTime fromPurchaseDate, DateTime toPurchaseDate, bool asNoTracking = false)
        {
            var products = asNoTracking ?
                await Entities
                    .AsNoTracking<VideoGame>()
                    .Where(entity => entity.PurchaseDate >= fromPurchaseDate && entity.PurchaseDate <= toPurchaseDate)
                    .ToListAsync() :
                await Entities
                    .Where(entity => entity.PurchaseDate >= fromPurchaseDate && entity.PurchaseDate <= toPurchaseDate)
                    .ToListAsync();

            return products;
        }

        public async Task<IEnumerable<VideoGame>> ReadByReleaseDateAsync(DateTime fromReleaseDate, DateTime toReleaseDate, bool asNoTracking = false)
        {
            var products = asNoTracking ?
                await Entities
                    .AsNoTracking<VideoGame>()
                    .Where(entity => entity.ReleaseDate >= fromReleaseDate && entity.ReleaseDate <= toReleaseDate)
                    .ToListAsync() :
                await Entities
                    .Where(entity => entity.ReleaseDate >= fromReleaseDate && entity.ReleaseDate <= toReleaseDate)
                    .ToListAsync();

            return products;
        }

        public async Task<VideoGame> ReadByTitleAsync(string title, bool asNoTracking = false)
        {
            var product = asNoTracking ?
                await Entities
                    .AsNoTracking<VideoGame>()
                    .Where(entity => entity.Title == title)
                    .SingleOrDefaultAsync() :
                await Entities
                    .Where(entity => entity.Title == title)
                    .SingleOrDefaultAsync();

            if (product == null)
            {
                return new VideoGame();
            }

            return product;
        }
    }
}
