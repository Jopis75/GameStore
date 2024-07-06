using Domain.Dtos;
using Domain.Entities;
using Domain.Filters;

namespace Application.Interfaces.Persistance
{
    public interface ICompanyRepository : IRepositoryBase<Company, CompanyDto, CompanyFilter>
    {
        Task<IEnumerable<CompanyDto>> ReadByEmailAddressAsync(string email, CancellationToken cancellationToken);

        Task<IEnumerable<CompanyDto>> ReadByNameAsync(string name, CancellationToken cancellationToken);

        Task<IEnumerable<CompanyDto>> ReadByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken);

        Task<IEnumerable<CompanyDto>> ReadByTradeNameAsync(string tradeName, CancellationToken cancellationToken);
    }
}
