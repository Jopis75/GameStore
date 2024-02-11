using Domain.Entities;

namespace Application.Interfaces.Persistance
{
    public interface IAddressRepository : IRepositoryBase<Address>
    {
        Task<IEnumerable<Address>> ReadByCityAsync(string city, bool asNoTracking = false);

        Task<Address> ReadByStreetAddressAsync(string streetAddress, bool asNoTracking = false);

        Task<IEnumerable<Address>> ReadByZipCodeAsync(string postalCode, bool asNoTracking = false);
    }
}
