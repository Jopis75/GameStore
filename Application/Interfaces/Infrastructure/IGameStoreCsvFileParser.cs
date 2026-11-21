using GameStore.Application.Dtos.General;

namespace GameStore.Application.Interfaces.Infrastructure
{
    public interface IGameStoreCsvFileParser
    {
        Task<IEnumerable<GameStoreCsvFileDto>> ParseAsync(string[] rows, CancellationToken cancellationToken);

        Task<GameStoreCsvFileDto> ParseAsync(string row, CancellationToken cancellationToken);
    }
}
