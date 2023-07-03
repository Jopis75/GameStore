using Domain.Entities;

namespace Application.Interfaces.Persistance
{
    public interface IAddressRepository : IRepositoryBase<Address>
    {
        Task<IEnumerable<Address>> GetByCityAsync(string city, bool asNoTracking = false);

        Task<IEnumerable<Address>> GetByZipCodeAsync(string postalCode, bool asNoTracking = false);
    }
}
