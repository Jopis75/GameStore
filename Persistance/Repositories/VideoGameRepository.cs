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
            var videoGame = asNoTracking ?
                await Entities
                    .AsNoTracking<VideoGame>()
                    .Where(videoGame1 => videoGame1.DeveloperId == developerId)
                    .ToListAsync() :
                await Entities
                    .Where(videoGame1 => videoGame1.DeveloperId == developerId)
                    .ToListAsync();

            return videoGame;
        }

        public async Task<IEnumerable<VideoGame>> ReadByPriceAsync(decimal fromPrice, decimal toPrice, bool asNoTracking = false)
        {
            var videoGames = asNoTracking ?
                await Entities
                    .AsNoTracking<VideoGame>()
                    .Where(videoGame1 => videoGame1.Price >= fromPrice && videoGame1.Price <= toPrice)
                    .ToListAsync() :
                await Entities
                    .Where(videoGame1 => videoGame1.Price >= fromPrice && videoGame1.Price <= toPrice)
                    .ToListAsync();

            return videoGames;
        }

        public async Task<IEnumerable<VideoGame>> ReadByPurchaseDateAsync(DateTime fromPurchaseDate, DateTime toPurchaseDate, bool asNoTracking = false)
        {
            var videoGames = asNoTracking ?
                await Entities
                    .AsNoTracking<VideoGame>()
                    .Where(videoGame1 => videoGame1.PurchaseDate >= fromPurchaseDate && videoGame1.PurchaseDate <= toPurchaseDate)
                    .ToListAsync() :
                await Entities
                    .Where(videoGame1 => videoGame1.PurchaseDate >= fromPurchaseDate && videoGame1.PurchaseDate <= toPurchaseDate)
                    .ToListAsync();

            return videoGames;
        }

        public async Task<IEnumerable<VideoGame>> ReadByReleaseDateAsync(DateTime fromReleaseDate, DateTime toReleaseDate, bool asNoTracking = false)
        {
            var videoGames = asNoTracking ?
                await Entities
                    .AsNoTracking<VideoGame>()
                    .Where(videoGame1 => videoGame1.ReleaseDate >= fromReleaseDate && videoGame1.ReleaseDate <= toReleaseDate)
                    .ToListAsync() :
                await Entities
                    .Where(videoGame1 => videoGame1.ReleaseDate >= fromReleaseDate && videoGame1.ReleaseDate <= toReleaseDate)
                    .ToListAsync();

            return videoGames;
        }

        public async Task<VideoGame> ReadByTitleAsync(string title, bool asNoTracking = false)
        {
            var videoGame = asNoTracking ?
                await Entities
                    .AsNoTracking<VideoGame>()
                    .Where(videoGame1 => videoGame1.Title == title)
                    .SingleOrDefaultAsync() :
                await Entities
                    .Where(videoGame1 => videoGame1.Title == title)
                    .SingleOrDefaultAsync();

            if (videoGame == null)
            {
                return new VideoGame();
            }

            return videoGame;
        }

        public async Task<VideoGame> ReadMostPlayedByConsoleIdAsync(int consoleId, bool asNoTracking = false)
        {
            var videoGame = asNoTracking ?
                await Entities
                    .AsNoTracking<VideoGame>()
                    .Where(videoGame1 => videoGame1.ConsoleVideoGames.Any(consoleVideoGame => consoleVideoGame.ConsoleId == consoleId))
                    .OrderByDescending(x => x.TotalTimePlayed)
                    .FirstOrDefaultAsync() :
                await Entities
                    .Where(videoGame1 => videoGame1.ConsoleVideoGames.Any(consoleVideoGame => consoleVideoGame.ConsoleId == consoleId))
                    .OrderByDescending(x => x.TotalTimePlayed)
                    .FirstOrDefaultAsync();

            if (videoGame == null)
            {
                return new VideoGame();
            }

            return videoGame;
        }
    }
}
