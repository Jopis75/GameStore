using Console = Domain.Entities.Console;

namespace Application.Interfaces.Persistance
{
    public interface IConsoleRepository : IRepositoryBase<Console>
    {
        Task<Console> ReadByNameAsync(string name, bool asNoTracking = false);
    }
}
