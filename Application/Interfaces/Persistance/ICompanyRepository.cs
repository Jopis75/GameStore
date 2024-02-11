using Domain.Entities;

namespace Application.Interfaces.Persistance
{
    public interface ICompanyRepository : IRepositoryBase<Company>
    {
        Task<Company> ReadByEmailAddressAsync(string email, bool asNoTracking = false);

        Task<Company> ReadByNameAsync(string name, bool asNoTracking = false);

        Task<Company> ReadByPhoneNumberAsync(string phoneNumber, bool asNoTracking = false);

        Task<Company> ReadByTradeNameAsync(string tradeName, bool asNoTracking = false);
    }
}
