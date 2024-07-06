using Domain.Dtos;
using Domain.Entities;
using Domain.Filters;

namespace Application.Interfaces.Persistance
{
    public interface IConsoleVideoGameRepository : IRepositoryBase<ConsoleVideoGame, ConsoleVideoGameDto, ConsoleVideoGameFilter>
    {
        Task<IEnumerable<ConsoleVideoGameDto>> ReadByConsoleIdAsync(int consoleId, CancellationToken cancellationToken);

        Task<IEnumerable<ConsoleVideoGameDto>> ReadByVideoGameIdAsync(int videoGameId, CancellationToken cancellationToken);
    }
}
