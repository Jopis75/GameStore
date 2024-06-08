using Domain.Dtos;
using Domain.Entities;
using Domain.Filters;

namespace Application.Interfaces.Persistance
{
    public interface ICompanyRepository : IRepositoryBase<Company, CompanyDto, CompanyFilter>
    {
        Task<IEnumerable<CompanyDto>> ReadByEmailAddressAsync(string email, bool asNoTracking = false);

        Task<IEnumerable<CompanyDto>> ReadByNameAsync(string name, bool asNoTracking = false);

        Task<IEnumerable<CompanyDto>> ReadByPhoneNumberAsync(string phoneNumber, bool asNoTracking = false);

        Task<IEnumerable<CompanyDto>> ReadByTradeNameAsync(string tradeName, bool asNoTracking = false);
    }
}
