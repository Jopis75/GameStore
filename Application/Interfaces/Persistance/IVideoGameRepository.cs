using Domain.Dtos;
using Domain.Entities;
using Domain.Filters;

namespace Application.Interfaces.Persistance
{
    public interface IVideoGameRepository : IRepositoryBase<VideoGame, VideoGameDto, VideoGameFilter>
    {
        Task<IEnumerable<VideoGameDto>> ReadByConsoleIdAsync(int consoleId, CancellationToken cancellationToken);

        Task<IEnumerable<VideoGameDto>> ReadByDeveloperIdAsync(int developerId, CancellationToken cancellationToken);

        Task<IEnumerable<VideoGameDto>> ReadByPriceAsync(decimal fromPrice, decimal toPrice, CancellationToken cancellationToken);

        Task<IEnumerable<VideoGameDto>> ReadByPurchaseDateAsync(DateTime fromPurchaseDate, DateTime toPurchaseDate, CancellationToken cancellationToken);

        Task<IEnumerable<VideoGameDto>> ReadByReleaseDateAsync(DateTime fromReleaseDate, DateTime toReleaseDate, CancellationToken cancellationToken);

        Task<IEnumerable<VideoGameDto>> ReadByTitleAsync(string title, CancellationToken cancellationToken);

        Task<VideoGameDto> ReadMostPlayedByConsoleIdAsync(int consoleId, CancellationToken cancellationToken);
    }
}
