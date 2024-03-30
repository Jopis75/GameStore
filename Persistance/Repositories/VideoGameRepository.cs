using Application.Interfaces.Persistance;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistance.DbContexts;

namespace Persistance.Repositories
{
    public class VideoGameRepository : RepositoryBase<VideoGame>, IVideoGameRepository
    {
        public VideoGameRepository(GameStoreDbContext gameStoreDbContext)
            : base(gameStoreDbContext)
        { }

        public async Task<IEnumerable<VideoGame>> ReadByDeveloperIdAsync(int developerId, bool asNoTracking = false)
        {
            var videoGames = asNoTracking ?
                await Entities
                    .AsNoTracking<VideoGame>()
                    .Where(videoGame => videoGame.DeveloperId == developerId)
                    .ToListAsync() :
                await Entities
                    .Where(videoGame => videoGame.DeveloperId == developerId)
                    .ToListAsync();

            return videoGames;
        }

        public async Task<IEnumerable<VideoGame>> ReadByPriceAsync(decimal fromPrice, decimal toPrice, bool asNoTracking = false)
        {
            var videoGames = asNoTracking ?
                await Entities
                    .AsNoTracking<VideoGame>()
                    .Where(videoGame => videoGame.Price >= fromPrice && videoGame.Price <= toPrice)
                    .ToListAsync() :
                await Entities
                    .Where(videoGame => videoGame.Price >= fromPrice && videoGame.Price <= toPrice)
                    .ToListAsync();

            return videoGames;
        }

        public async Task<IEnumerable<VideoGame>> ReadByPurchaseDateAsync(DateTime fromPurchaseDate, DateTime toPurchaseDate, bool asNoTracking = false)
        {
            var videoGames = asNoTracking ?
                await Entities
                    .AsNoTracking<VideoGame>()
                    .Where(videoGame => videoGame.PurchaseDate >= fromPurchaseDate && videoGame.PurchaseDate <= toPurchaseDate)
                    .ToListAsync() :
                await Entities
                    .Where(videoGame => videoGame.PurchaseDate >= fromPurchaseDate && videoGame.PurchaseDate <= toPurchaseDate)
                    .ToListAsync();

            return videoGames;
        }

        public async Task<IEnumerable<VideoGame>> ReadByReleaseDateAsync(DateTime fromReleaseDate, DateTime toReleaseDate, bool asNoTracking = false)
        {
            var videoGames = asNoTracking ?
                await Entities
                    .AsNoTracking<VideoGame>()
                    .Where(videoGame => videoGame.ReleaseDate >= fromReleaseDate && videoGame.ReleaseDate <= toReleaseDate)
                    .ToListAsync() :
                await Entities
                    .Where(videoGame => videoGame.ReleaseDate >= fromReleaseDate && videoGame.ReleaseDate <= toReleaseDate)
                    .ToListAsync();

            return videoGames;
        }

        public async Task<VideoGame> ReadByTitleAsync(string title, bool asNoTracking = false)
        {
            var videoGame = asNoTracking ?
                await Entities
                    .AsNoTracking<VideoGame>()
                    .Where(videoGame => videoGame.Title == title)
                    .SingleOrDefaultAsync() :
                await Entities
                    .Where(videoGame => videoGame.Title == title)
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
                    .Where(videoGame => videoGame.ConsoleVideoGames.Any(consoleVideoGame => consoleVideoGame.ConsoleId == consoleId))
                    .OrderByDescending(videoGame => videoGame.TotalTimePlayed)
                    .FirstOrDefaultAsync() :
                await Entities
                    .Where(videoGame => videoGame.ConsoleVideoGames.Any(consoleVideoGame => consoleVideoGame.ConsoleId == consoleId))
                    .OrderByDescending(videoGame => videoGame.TotalTimePlayed)
                    .FirstOrDefaultAsync();

            if (videoGame == null)
            {
                return new VideoGame();
            }

            return videoGame;
        }
    }
}
