using Application.Interfaces.Persistance;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistance.DbContexts;

namespace Persistance.Repositories
{
    public class ConsoleProductRepository : RepositoryBase<ConsoleProduct>, IConsoleProductRepository
    {
        public ConsoleProductRepository(GameStoreDbContext gameStoreDbContext)
            : base(gameStoreDbContext) { }

        public async Task<IEnumerable<ConsoleProduct>> ReadByConsoleIdAsync(int consoleId, bool asNoTracking = false)
        {
            var consoleProducts = asNoTracking ?
                await Entities
                    .AsNoTracking()
                    .Where(entity => entity.ConsoleId == consoleId)
                    .ToListAsync() :
                await Entities
                    .Where(entity => entity.ConsoleId == consoleId)
                    .ToListAsync();

            return consoleProducts;
        }

        public async Task<IEnumerable<ConsoleProduct>> ReadByProductIdAsync(int productId, bool asNoTracking = false)
        {
            var consoleProducts = asNoTracking ?
                await Entities
                    .AsNoTracking()
                    .Where(entity => entity.ProductId == productId)
                    .ToListAsync() :
                await Entities
                    .Where(entity => entity.ProductId == productId)
                    .ToListAsync();

            return consoleProducts;
        }
    }
}
