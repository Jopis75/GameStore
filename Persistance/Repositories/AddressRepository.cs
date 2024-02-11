﻿using Application.Interfaces.Persistance;
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

        public async Task<Address> ReadByStreetAddressAsync(string streetAddress, bool asNoTracking = false)
        {
            var address = asNoTracking ?
                await Entities
                    .AsNoTracking<Address>()
                    .Where(entity => entity.StreetAddress == streetAddress)
                    .SingleOrDefaultAsync() :
                await Entities
                    .Where(entity => entity.StreetAddress == streetAddress)
                    .SingleOrDefaultAsync();

            if (address == null)
            {
                return new Address();
            }

            return address;
        }

        public async Task<IEnumerable<Address>> ReadByZipCodeAsync(string postalCode, bool asNoTracking = false)
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
