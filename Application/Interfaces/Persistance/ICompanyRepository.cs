using Domain.Dtos;
using Domain.Entities;
using Domain.Filters;

namespace Application.Interfaces.Persistance
{
    public interface ICompanyRepository : IRepositoryBase<Company, CompanyDto, CompanyFilter>
    {
        Task<CompanyDto> ReadByEmailAddressAsync(string email, CancellationToken cancellationToken);

        Task<CompanyDto> ReadByNameAsync(string name, CancellationToken cancellationToken);

        Task<CompanyDto> ReadByPhoneNumberAsync(string phoneNumber, CancellationToken cancellationToken);

        Task<CompanyDto> ReadByTradeNameAsync(string tradeName, CancellationToken cancellationToken);
    }
}
