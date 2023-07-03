using Domain.Entities;

namespace Application.Interfaces.Persistance
{
    public interface ICompanyRepository : IRepositoryBase<Company>
    {
        Task<IEnumerable<Company>> GetByFoundedAsync(DateTime founded, bool asNoTracking = false);

        Task<Company> GetByNameAsync(string name, bool asNoTracking = false);

        Task<IEnumerable<Company>> GetByNumberOfEmployeesAsync(int numberOfEmployees, bool asNoTracking = false);

        Task<IEnumerable<Company>> GetByNumberOfEmployeesAsync(int fromNumberOfEmployees, int toNumberOfEmployees, bool asNoTracking = false);

        Task<Company> GetByTradeNameAsync(string tradeName, bool asNoTracking = false);
    }
}
