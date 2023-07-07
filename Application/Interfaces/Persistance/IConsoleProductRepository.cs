using Domain.Entities;

namespace Application.Interfaces.Persistance
{
    public interface IConsoleProductRepository : IRepositoryBase<ConsoleProduct>
    {
        Task<IEnumerable<ConsoleProduct>> ReadByConsoleIdAsync(int consoleId, bool asNoTracking = false);

        Task<IEnumerable<ConsoleProduct>> ReadByProductIdAsync(int productId, bool asNoTracking = false);
    }
}
