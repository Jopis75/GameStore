using Domain.Entities;
using Domain.Filters;

namespace Application.Interfaces.Persistance
{
    public interface IAddressRepository : IRepositoryBase<Address, AddressFilter>
    {
        Task<IEnumerable<Address>> ReadByCityAsync(string city, bool asNoTracking = false);

        Task<IEnumerable<Address>> ReadByStreetAddressAsync(string streetAddress, bool asNoTracking = false);

        Task<IEnumerable<Address>> ReadByPostalCodeAsync(string postalCode, bool asNoTracking = false);
    }
}
