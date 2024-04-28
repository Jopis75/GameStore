using Application.Interfaces.Persistance;
using Domain.Entities;
using Domain.Filters;
using Microsoft.EntityFrameworkCore;
using Persistance.DbContexts;
using System.Linq.Expressions;

namespace Persistance.Repositories
{
    public class VideoGameRepository : RepositoryBase<VideoGame, VideoGameFilter>, IVideoGameRepository
    {
        public VideoGameRepository(GameStoreDbContext gameStoreDbContext)
            : base(gameStoreDbContext) { }
        
        public async Task<IEnumerable<VideoGame>> ReadByConsoleIdAsync(int consoleId, bool asNoTracking = false)
        {
            var query = Entities.AsQueryable();

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            var videoGames = await query
                .Include(videoGame => videoGame.ConsoleVideoGames)
                    .ThenInclude(consoleVideoGame => consoleVideoGame.Console)
                .Include(videoGame => videoGame.Reviews)
                .Include(videoGame => videoGame.Developer)
                    .ThenInclude(developer => developer.Headquarter)
                .Where(videoGame => videoGame.ConsoleVideoGames.Any(consoleVideoGame => consoleVideoGame.ConsoleId == consoleId))
                .ToListAsync();

            return videoGames;
        }

        public async Task<IEnumerable<VideoGame>> ReadByDeveloperIdAsync(int developerId, bool asNoTracking = false)
        {
            var query = Entities.AsQueryable();

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            var videoGames = await query
                .Where(videoGame => videoGame.DeveloperId == developerId)
                .ToListAsync();

            return videoGames;
        }

        protected override Task<IEnumerable<VideoGame>> ReadByFilterAsync(VideoGameFilter filter, IQueryable<VideoGame> query, Expression<Func<VideoGame, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<VideoGame>> ReadByPriceAsync(decimal fromPrice, decimal toPrice, bool asNoTracking = false)
        {
            var query = Entities.AsQueryable();

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            var videoGames = await query
                .Where(videoGame => videoGame.Price >= fromPrice && videoGame.Price <= toPrice)
                .ToListAsync();

            return videoGames;
        }

        public async Task<IEnumerable<VideoGame>> ReadByPurchaseDateAsync(DateTime fromPurchaseDate, DateTime toPurchaseDate, bool asNoTracking = false)
        {
            var query = Entities.AsQueryable();

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            var videoGames = await query
                .Where(videoGame => videoGame.PurchaseDate.Date >= fromPurchaseDate.Date && videoGame.PurchaseDate.Date <= toPurchaseDate.Date)
                .ToListAsync();

            return videoGames;
        }

        public async Task<IEnumerable<VideoGame>> ReadByReleaseDateAsync(DateTime fromReleaseDate, DateTime toReleaseDate, bool asNoTracking = false)
        {
            var query = Entities.AsQueryable();

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            var videoGames = await query
                .Where(videoGame => videoGame.ReleaseDate.Date >= fromReleaseDate.Date && videoGame.ReleaseDate.Date <= toReleaseDate.Date)
                .ToListAsync();

            return videoGames;
        }

        public async Task<IEnumerable<VideoGame>> ReadByTitleAsync(string title, bool asNoTracking = false)
        {
            var query = Entities.AsQueryable();

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            var videoGames = await query
                .Where(videoGame => EF.Functions.Like(videoGame.Title, $"{title}%"))
                .ToListAsync();

            return videoGames;
        }

        public async Task<VideoGame> ReadMostPlayedByConsoleIdAsync(int consoleId, bool asNoTracking = false)
        {
            var query = Entities.AsQueryable();

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            var videoGame = await query
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
