using Abp.Linq.Expressions;
using Application.Interfaces.Persistance;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Filters;
using Microsoft.EntityFrameworkCore;
using Persistance.DbContexts;
using System.Linq.Expressions;

namespace Persistance.Repositories
{
    public class VideoGameRepository : RepositoryBase<VideoGame, VideoGameDto, VideoGameFilter>, IVideoGameRepository
    {
        public VideoGameRepository(GameStoreDbContext gameStoreDbContext, IMapper mapper)
            : base(gameStoreDbContext, mapper)
        {
        }

        public async Task<IEnumerable<VideoGameDto>> ReadByConsoleIdAsync(int consoleId, CancellationToken cancellationToken)
        {
           cancellationToken.ThrowIfCancellationRequested();

            var videoGames = await Entities
                .AsNoTracking()
                .Include(videoGame => videoGame.ConsoleVideoGames)
                    .ThenInclude(consoleVideoGame => consoleVideoGame.Console)
                .Include(videoGame => videoGame.Reviews)
                .Include(videoGame => videoGame.Developer)
                    .ThenInclude(developer => developer.Headquarter)
                .Where(videoGame => videoGame.ConsoleVideoGames.Any(consoleVideoGame => consoleVideoGame.ConsoleId == consoleId))
                .ToArrayAsync();

            return videoGames.Select(Mapper.Map<VideoGameDto>);
        }

        public async Task<IEnumerable<VideoGameDto>> ReadByDeveloperIdAsync(int developerId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var videoGames = await Entities
                .AsNoTracking()
                .Where(videoGame => videoGame.DeveloperId == developerId)
                .ToArrayAsync();

            return videoGames.Select(Mapper.Map<VideoGameDto>);
        }

        protected override async Task<IEnumerable<VideoGameDto>> ReadByFilterAsync(VideoGameFilter filter, Expression<Func<VideoGame, bool>> predicate, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (filter.DeveloperId != null)
            {
                predicate = predicate.And(console => console.DeveloperId == filter.DeveloperId);
            }

            if (filter.ImageUri != null)
            {
                predicate = predicate.And(console => console.ImageUri != null && EF.Functions.Like(console.ImageUri, $"{filter.ImageUri}%"));
            }

            if (filter.Name != null)
            {
                predicate = predicate.And(console => EF.Functions.Like(console.Name, $"{filter.Name}%"));
            }

            if (filter.Price != null)
            {
                predicate = predicate.And(console => console.Price == filter.Price);
            }

            if (filter.PurchaseDate != null)
            {
                predicate = predicate.And(console => console.PurchaseDate.Date == filter.PurchaseDate.Value.Date);
            }

            if (filter.ReleaseDate != null)
            {
                predicate = predicate.And(console => console.ReleaseDate.Date == filter.ReleaseDate.Value.Date);
            }

            if (filter.Url != null)
            {
                predicate = predicate.And(console => console.Url != null && EF.Functions.Like(console.Url, $"{filter.Url}%"));
            }

            if (filter.Title != null)
            {
                predicate = predicate.And(videoGame => EF.Functions.Like(videoGame.Title, $"{filter.Title}%"));
            }

            var videoGames = await Entities
                .AsNoTracking()
                .Where(predicate)
                .ToArrayAsync();

            return videoGames.Select(Mapper.Map<VideoGameDto>);
        }

        public async Task<IEnumerable<VideoGameDto>> ReadByPriceAsync(decimal fromPrice, decimal toPrice, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var videoGames = await Entities
                .AsNoTracking()
                .Where(videoGame => videoGame.Price >= fromPrice && videoGame.Price <= toPrice)
                .ToArrayAsync();

            return videoGames.Select(Mapper.Map<VideoGameDto>);
        }

        public async Task<IEnumerable<VideoGameDto>> ReadByPurchaseDateAsync(DateTime fromPurchaseDate, DateTime toPurchaseDate, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var videoGames = await Entities
                .AsNoTracking()
                .Where(videoGame => videoGame.PurchaseDate.Date >= fromPurchaseDate.Date && videoGame.PurchaseDate.Date <= toPurchaseDate.Date)
                .ToArrayAsync();

            return videoGames.Select(Mapper.Map<VideoGameDto>);
        }

        public async Task<IEnumerable<VideoGameDto>> ReadByReleaseDateAsync(DateTime fromReleaseDate, DateTime toReleaseDate, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var videoGames = await Entities
                .AsNoTracking()
                .Where(videoGame => videoGame.ReleaseDate.Date >= fromReleaseDate.Date && videoGame.ReleaseDate.Date <= toReleaseDate.Date)
                .ToArrayAsync();

            return videoGames.Select(Mapper.Map<VideoGameDto>);
        }

        public async Task<IEnumerable<VideoGameDto>> ReadByTitleAsync(string title, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var videoGames = await Entities
                .AsNoTracking()
                .Where(videoGame => EF.Functions.Like(videoGame.Title, $"{title}%"))
                .ToArrayAsync();

            return videoGames.Select(Mapper.Map<VideoGameDto>);
        }

        public async Task<VideoGameDto> ReadMostPlayedByConsoleIdAsync(int consoleId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var videoGame = await Entities
                .AsNoTracking()
                .Include(videoGame => videoGame.Developer)
                    .ThenInclude(company => company.Headquarter)
                .Include(videoGame => videoGame.Reviews)
                .Include(videoGame => videoGame.ConsoleVideoGames)
                    .ThenInclude(consoleVidoeGame => consoleVidoeGame.Console)
                //.Include(videoGame => videoGame.ConsoleVideoGames)
                //    .ThenInclude(consoleVidoeGame => consoleVidoeGame.VideoGame)
                .Include(videoGame => videoGame.VideoGameGenres)
                    .ThenInclude(videoGameGenre => videoGameGenre.Genre)
                .Include(videoGame => videoGame.Trophies)
                .Where(videoGame => videoGame.ConsoleVideoGames.Any(consoleVideoGame => consoleVideoGame.ConsoleId == consoleId))
                .OrderByDescending(videoGame => videoGame.TotalTimePlayed)
                .FirstOrDefaultAsync();

            if (videoGame == null)
            {
                return new VideoGameDto();
            }

            return Mapper.Map<VideoGameDto>(videoGame);
        }
    }
}
