using GameStore.Domain.Dtos;
using GameStore.Domain.Entities;
using GameStore.Domain.Filters;

namespace GameStore.Application.Interfaces.Persistance
{
    public interface ICompanyRepository : IRepositoryBase<Company, CompanyDto, CompanyFilter>
    {
        Task<CompanyDto> ReadByEmailAddressAsync(string email, CancellationToken cancellationToken);

        Task<IEnumerable<CompanyDto>> ReadByNameAsync(string name, CancellationToken cancellationToken);

        Task<CompanyDto> ReadByNameExactAsync(string name, CancellationToken cancellationToken);

        Task<IEnumerable<CompanyDto>> ReadByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken);

        Task<IEnumerable<CompanyDto>> ReadByTradeNameAsync(string tradeName, CancellationToken cancellationToken);
    }
}
