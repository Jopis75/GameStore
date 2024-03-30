using Application.Interfaces.Persistance;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistance.DbContexts;

namespace Persistance.Repositories
{
    public class ConsoleVideoGameRepository : RepositoryBase<ConsoleVideoGame>, IConsoleVideoGameRepository
    {
        public ConsoleVideoGameRepository(GameStoreDbContext gameStoreDbContext)
            : base(gameStoreDbContext)
        { }

        public async Task<IEnumerable<ConsoleVideoGame>> ReadByConsoleIdAsync(int consoleId, bool asNoTracking = false)
        {
            var consoleVideoGames = asNoTracking ?
                await Entities
                    .AsNoTracking()
                    .Where(consoleVideoGame => consoleVideoGame.ConsoleId == consoleId)
                    .ToListAsync() :
                await Entities
                    .Where(consoleVideoGame => consoleVideoGame.ConsoleId == consoleId)
                    .ToListAsync();

            return consoleVideoGames;
        }

        public async Task<IEnumerable<ConsoleVideoGame>> ReadByVideoGameIdAsync(int videoGameId, bool asNoTracking = false)
        {
            var consoleVideoGames = asNoTracking ?
                await Entities
                    .AsNoTracking()
                    .Where(consoleVideoGame => consoleVideoGame.VideoGameId == videoGameId)
                    .ToListAsync() :
                await Entities
                    .Where(consoleVideoGame => consoleVideoGame.VideoGameId == videoGameId)
                    .ToListAsync();

            return consoleVideoGames;
        }
    }
}
