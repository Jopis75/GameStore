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

        public async Task<IEnumerable<Address>> ReadByCityAsync(string city, bool asNoTracking = false)
        {
            var query = Entities.AsQueryable();

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            var addresses = await query
                .Where(address => EF.Functions.Like(address.City, $"{city}%"))
                .ToListAsync();

            return addresses;
        }

        public async Task<IEnumerable<Address>> ReadByStreetAddressAsync(string streetAddress, bool asNoTracking = false)
        {
            var query = Entities.AsQueryable();

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            var addresses = await query
                .Where(address => EF.Functions.Like(address.StreetAddress, $"{streetAddress}%"))
                .ToListAsync();

            return addresses;
        }

        public async Task<IEnumerable<Address>> ReadByZipCodeAsync(string postalCode, bool asNoTracking = false)
        {
            var query = Entities.AsQueryable();

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            var addresses = await query
                .Where(address => EF.Functions.Like(address.PostalCode, $"{postalCode}%"))
                .ToListAsync();

            return addresses;
        }
    }
}
