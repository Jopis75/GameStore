using Application.Interfaces.Persistance;
using Microsoft.EntityFrameworkCore;
using Persistance.DbContexts;
using Console = Domain.Entities.Console;

namespace Persistance.Repositories
{
    public class ConsoleRepository : RepositoryBase<Console>, IConsoleRepository
    {
        public ConsoleRepository(GameStoreDbContext gameStoreDbContext)
            : base(gameStoreDbContext) { }

        public async Task<Console> ReadByNameAsync(string name, bool include = false, bool asNoTracking = false)
        {
            var entities = asNoTracking ? Entities.AsNoTracking() : Entities;

            var console = include ?
                await entities
                    .Include(entity => entity.ConsoleProducts)
                    .Include(entity => entity.Developer)
                    .Include(entity => entity.Review)
                    .Where(entity => entity.Name == name)
                    .SingleOrDefaultAsync() :
                await entities
                    .Where(entity => entity.Name == name)
                    .SingleOrDefaultAsync();

            if (console == null)
            {
                return new Console();
            }

            return console;
        }
    }
}
