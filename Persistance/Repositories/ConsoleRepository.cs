using Application.Interfaces.Persistance;
using Domain.Filters;
using Microsoft.EntityFrameworkCore;
using Persistance.DbContexts;
using Console = Domain.Entities.Console;

namespace Persistance.Repositories
{
    public class ConsoleRepository : RepositoryBase<Console, ConsoleFilter>, IConsoleRepository
    {
        public ConsoleRepository(GameStoreDbContext gameStoreDbContext)
            : base(gameStoreDbContext) { }

        public override Task<IEnumerable<Console>> ReadByFilterAsync(ConsoleFilter filter, bool asNoTracking = false)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Console>> ReadByNameAsync(string name, bool asNoTracking = false)
        {
            var query = Entities.AsQueryable();

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            var consoles = await query
                .Where(console => EF.Functions.Like(console.Name, $"{name}%"))
                .ToListAsync();

            return consoles;
        }
    }
}
