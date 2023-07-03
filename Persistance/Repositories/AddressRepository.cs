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

        public async Task<IEnumerable<Address>> GetByCityAsync(string city, bool asNoTracking = false)
        {
            var addresses = asNoTracking ?
                await Entities
                    .AsNoTracking<Address>()
                    .Where(entity => entity.City == city)
                    .ToListAsync() :
                await Entities
                    .Where(entity => entity.City == city)
                    .ToListAsync();

            return addresses;
        }

        public async Task<IEnumerable<Address>> GetByZipCodeAsync(string postalCode, bool asNoTracking = false)
        {
            var addresses = asNoTracking ?
                await Entities
                    .AsNoTracking<Address>()
                    .Where(entity => entity.PostalCode == postalCode)
                    .ToListAsync() :
                await Entities
                    .Where(entity => entity.PostalCode == postalCode)
                    .ToListAsync();

            return addresses;
        }
    }
}
