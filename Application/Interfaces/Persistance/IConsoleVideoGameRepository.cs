using Domain.Entities;
using Domain.Filters;

namespace Application.Interfaces.Persistance
{
    public interface IConsoleVideoGameRepository : IRepositoryBase<ConsoleVideoGame, ConsoleVideoGameFilter>
    {
        Task<IEnumerable<ConsoleVideoGame>> ReadByConsoleIdAsync(int consoleId, bool asNoTracking = false);

        Task<IEnumerable<ConsoleVideoGame>> ReadByVideoGameIdAsync(int videoGameId, bool asNoTracking = false);
    }
}
