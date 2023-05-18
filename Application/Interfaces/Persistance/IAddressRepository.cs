using Domain.Entities;

namespace Application.Interfaces.Persistance
{
    public interface IAddressRepository : IRepositoryBase<Address>
    {
        Task<IEnumerable<Address>> GetByCityAsync(string city);

        Task<IEnumerable<Address>> GetByZipCodeAsync(string zipCode);
    }
}
