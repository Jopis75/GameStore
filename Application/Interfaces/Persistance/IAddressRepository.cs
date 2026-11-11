using GameStore.Domain.Dtos;
using GameStore.Domain.Entities;
using GameStore.Domain.Filters;

namespace GameStore.Application.Interfaces.Persistance
{
    public interface IAddressRepository : IRepositoryBase<Address, AddressDto, AddressFilter>
    {
        Task<bool> IsUniqueAsync(string streetAddress, string postalCode, string city, CancellationToken cancellationToken);

        Task<IEnumerable<AddressDto>> ReadByCityAsync(string city, CancellationToken cancellationToken);

        Task<IEnumerable<AddressDto>> ReadByStreetAddressAsync(string streetAddress, CancellationToken cancellationToken);

        Task<IEnumerable<AddressDto>> ReadByPostalCodeAsync(string postalCode, CancellationToken cancellationToken);
    }
}
