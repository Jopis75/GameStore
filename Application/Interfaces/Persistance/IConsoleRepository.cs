using Domain.Dtos;
using Domain.Filters;
using Console = Domain.Entities.Console;

namespace Application.Interfaces.Persistance
{
    public interface IConsoleRepository : IRepositoryBase<Console, ConsoleDto, ConsoleFilter>
    {
        Task<IEnumerable<ConsoleDto>> ReadByNameAsync(string name, CancellationToken cancellationToken);
    }
}
