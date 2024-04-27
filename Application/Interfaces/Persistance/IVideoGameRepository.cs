using Domain.Entities;
using Domain.Filters;

namespace Application.Interfaces.Persistance
{
    public interface IVideoGameRepository : IRepositoryBase<VideoGame, VideoGameFilter>
    {
        Task<IEnumerable<VideoGame>> ReadByConsoleIdAsync(int consoleId, bool asNoTracking = false);

        Task<IEnumerable<VideoGame>> ReadByDeveloperIdAsync(int developerId, bool asNoTracking = false);

        Task<IEnumerable<VideoGame>> ReadByPriceAsync(decimal fromPrice, decimal toPrice, bool asNoTracking = false);

        Task<IEnumerable<VideoGame>> ReadByPurchaseDateAsync(DateTime fromPurchaseDate, DateTime toPurchaseDate, bool asNoTracking = false);

        Task<IEnumerable<VideoGame>> ReadByReleaseDateAsync(DateTime fromReleaseDate, DateTime toReleaseDate, bool asNoTracking = false);

        Task<IEnumerable<VideoGame>> ReadByTitleAsync(string title, bool asNoTracking = false);

        Task<VideoGame> ReadMostPlayedByConsoleIdAsync(int consoleId, bool asNoTracking = false);
    }
}
