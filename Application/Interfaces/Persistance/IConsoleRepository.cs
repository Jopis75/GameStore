using GameStore.Domain.Dtos;
using GameStore.Domain.Filters;
using Console = GameStore.Domain.Entities.Console;

namespace GameStore.Application.Interfaces.Persistance
{
    public interface IConsoleRepository : IRepositoryBase<Console, ConsoleDto, ConsoleFilter>
    {
        Task<IEnumerable<ConsoleDto>> ReadByNameAsync(string name, CancellationToken cancellationToken);

        Task<ConsoleDto> ReadByNameExactAsync(string name, CancellationToken cancellationToken);
    }
}
