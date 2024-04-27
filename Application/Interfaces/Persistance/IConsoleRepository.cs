using Domain.Filters;
using Console = Domain.Entities.Console;

namespace Application.Interfaces.Persistance
{
    public interface IConsoleRepository : IRepositoryBase<Console, ConsoleFilter>
    {
        Task<IEnumerable<Console>> ReadByNameAsync(string name, bool asNoTracking = false);
    }
}
