using Domain.Dtos;
using Domain.Entities;
using Domain.Filters;

namespace Application.Interfaces.Persistance
{
    public interface IAddressRepository : IRepositoryBase<Address, AddressDto, AddressFilter>
    {
        Task<IEnumerable<AddressDto>> ReadByCityAsync(string city, CancellationToken cancellationToken);

        Task<IEnumerable<AddressDto>> ReadByStreetAddressAsync(string streetAddress, CancellationToken cancellationToken);

        Task<IEnumerable<AddressDto>> ReadByPostalCodeAsync(string postalCode, CancellationToken cancellationToken);
    }
}
