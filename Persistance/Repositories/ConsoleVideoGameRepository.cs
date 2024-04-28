using Application.Interfaces.Persistance;
using Domain.Entities;
using Domain.Filters;
using Microsoft.EntityFrameworkCore;
using Persistance.DbContexts;
using System.Linq.Expressions;

namespace Persistance.Repositories
{
    public class ConsoleVideoGameRepository : RepositoryBase<ConsoleVideoGame, ConsoleVideoGameFilter>, IConsoleVideoGameRepository
    {
        public ConsoleVideoGameRepository(GameStoreDbContext gameStoreDbContext)
            : base(gameStoreDbContext) { }

        protected override Task<IEnumerable<ConsoleVideoGame>> ReadByFilterAsync(ConsoleVideoGameFilter filter, IQueryable<ConsoleVideoGame> query, Expression<Func<ConsoleVideoGame, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ConsoleVideoGame>> ReadByConsoleIdAsync(int consoleId, bool asNoTracking = false)
        {
            var query = Entities.AsQueryable();

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            var consoleVideoGames = await query
                .Where(consoleVideoGame => consoleVideoGame.ConsoleId == consoleId)
                .ToListAsync();

            return consoleVideoGames;
        }

        public async Task<IEnumerable<ConsoleVideoGame>> ReadByVideoGameIdAsync(int videoGameId, bool asNoTracking = false)
        {
            var query = Entities.AsQueryable();

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            var consoleVideoGames = await query
                .Where(consoleVideoGame => consoleVideoGame.VideoGameId == videoGameId)
                .ToListAsync();

            return consoleVideoGames;
        }
    }
}
