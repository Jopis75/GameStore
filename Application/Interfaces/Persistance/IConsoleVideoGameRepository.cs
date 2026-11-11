using GameStore.Domain.Dtos;
using GameStore.Domain.Entities;
using GameStore.Domain.Filters;

namespace GameStore.Application.Interfaces.Persistance
{
    public interface IConsoleVideoGameRepository : IRepositoryBase<ConsoleVideoGame, ConsoleVideoGameDto, ConsoleVideoGameFilter>
    {
        Task<IEnumerable<ConsoleVideoGameDto>> ReadByConsoleIdAsync(int consoleId, CancellationToken cancellationToken);

        Task<IEnumerable<ConsoleVideoGameDto>> ReadByVideoGameIdAsync(int videoGameId, CancellationToken cancellationToken);
    }
}
