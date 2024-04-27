using Domain.Entities;
using Domain.Filters;

namespace Application.Interfaces.Persistance
{
    public interface ICompanyRepository : IRepositoryBase<Company, CompanyFilter>
    {
        Task<IEnumerable<Company>> ReadByEmailAddressAsync(string email, bool asNoTracking = false);

        Task<IEnumerable<Company>> ReadByNameAsync(string name, bool asNoTracking = false);

        Task<IEnumerable<Company>> ReadByPhoneNumberAsync(string phoneNumber, bool asNoTracking = false);

        Task<IEnumerable<Company>> ReadByTradeNameAsync(string tradeName, bool asNoTracking = false);
    }
}
