using Domain.Entities;

namespace Application.Interfaces.Persistance
{
    public interface ICompanyRepository : IRepositoryBase<Company>
    {
        Task<IEnumerable<Company>> GetByFoundedAsync(int founded);

        Task<Company> GetByNameAsync(string name);

        Task<IEnumerable<Company>> GetByNumberOfEmployeesAsync(int numberOfEmployees);

        Task<IEnumerable<Company>> GetByNumberOfEmployeesAsync(int fromNumberOfEmployees, int toNumberOfEmployees);

        Task<Company> GetByTradeNameAsync(string tradeName);
    }
}
