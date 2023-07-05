using Domain.Entities;

namespace Application.Interfaces.Persistance
{
    public interface ICompanyRepository : IRepositoryBase<Company>
    {
        Task<Company> GetByNameAsync(string name, bool asNoTracking = false);

        Task<Company> GetByTradeNameAsync(string tradeName, bool asNoTracking = false);
    }
}
