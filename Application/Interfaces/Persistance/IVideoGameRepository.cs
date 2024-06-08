using Domain.Dtos;
using Domain.Entities;
using Domain.Filters;

namespace Application.Interfaces.Persistance
{
    public interface IVideoGameRepository : IRepositoryBase<VideoGame, VideoGameDto, VideoGameFilter>
    {
        Task<IEnumerable<VideoGameDto>> ReadByConsoleIdAsync(int consoleId, bool asNoTracking = false);

        Task<IEnumerable<VideoGameDto>> ReadByDeveloperIdAsync(int developerId, bool asNoTracking = false);

        Task<IEnumerable<VideoGameDto>> ReadByPriceAsync(decimal fromPrice, decimal toPrice, bool asNoTracking = false);

        Task<IEnumerable<VideoGameDto>> ReadByPurchaseDateAsync(DateTime fromPurchaseDate, DateTime toPurchaseDate, bool asNoTracking = false);

        Task<IEnumerable<VideoGameDto>> ReadByReleaseDateAsync(DateTime fromReleaseDate, DateTime toReleaseDate, bool asNoTracking = false);

        Task<IEnumerable<VideoGameDto>> ReadByTitleAsync(string title, bool asNoTracking = false);

        Task<VideoGameDto> ReadMostPlayedByConsoleIdAsync(int consoleId, bool asNoTracking = false);
    }
}
