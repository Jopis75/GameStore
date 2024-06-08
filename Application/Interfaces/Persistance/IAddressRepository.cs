using Domain.Dtos;
using Domain.Entities;
using Domain.Filters;

namespace Application.Interfaces.Persistance
{
    public interface IAddressRepository : IRepositoryBase<Address, AddressDto, AddressFilter>
    {
        Task<IEnumerable<AddressDto>> ReadByCityAsync(string city, bool asNoTracking = false);

        Task<IEnumerable<AddressDto>> ReadByStreetAddressAsync(string streetAddress, bool asNoTracking = false);

        Task<IEnumerable<AddressDto>> ReadByPostalCodeAsync(string postalCode, bool asNoTracking = false);
    }
}
