using Domain.Entities;

namespace Application.Interfaces.Persistance
{
    public interface ICompanyRepository : IRepositoryBase<Company>
    {
        Task<Company> ReadByNameAsync(string name, bool asNoTracking = false);

        Task<Company> ReadByTradeNameAsync(string tradeName, bool asNoTracking = false);
    }
}
