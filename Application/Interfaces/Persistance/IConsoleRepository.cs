using Console = Domain.Entities.Console;

namespace Application.Interfaces.Persistance
{
    public interface IConsoleRepository : IRepositoryBase<Console>
    {
        Task<Console> ReadByNameAsync(string name, bool include = false, bool asNoTracking = false);
    }
}
