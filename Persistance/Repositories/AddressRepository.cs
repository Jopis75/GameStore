using Application.Interfaces.Persistance;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistance.DbContexts;

namespace Persistance.Repositories
{
    public class AddressRepository : RepositoryBase<Address>, IAddressRepository
    {
        public AddressRepository(GameStoreDbContext gameStoreDbContext)
            : base(gameStoreDbContext) { }

        public async Task<IEnumerable<Address>> GetByCityAsync(string city)
        {
            var addresses = await Entities
                .Where(entity => entity.City == city)
                .ToListAsync();

            return addresses;
        }

        public async Task<IEnumerable<Address>> GetByZipCodeAsync(string postalCode)
        {
            var addresses = await Entities
                .Where(entity => entity.PostalCode == postalCode)
                .ToListAsync();

            return addresses;
        }
    }
}
