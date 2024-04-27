using Abp.Linq.Expressions;
using Application.Interfaces.Persistance;
using Domain.Entities;
using Domain.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Persistance.DbContexts;

namespace Persistance.Repositories
{
    public class AddressRepository : RepositoryBase<Address, AddressFilter>, IAddressRepository
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

        public override async Task<IEnumerable<Address>> ReadByFilterAsync(AddressFilter filter, bool asNoTracking = false)
        {
            var query = Entities.AsQueryable();

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            if (filter != null)
            {
                var predicateBuilder = PredicateBuilder.New<Address>();

                if (filter.StreetAddress != null)
                {
                    predicateBuilder = predicateBuilder.And(address => EF.Functions.Like(address.StreetAddress, $"{filter.StreetAddress}%"));
                }

                if (filter.City != null)
                {
                    predicateBuilder = predicateBuilder.And(address => EF.Functions.Like(address.City, $"{filter.City}%"));
                }

                if (filter.State != null)
                {
                    predicateBuilder = predicateBuilder.And(address => EF.Functions.Like(address.State, $"{filter.State}%"));
                }

                if (filter.PostalCode != null)
                {
                    predicateBuilder = predicateBuilder.And(address => EF.Functions.Like(address.PostalCode, $"{filter.PostalCode}%"));
                }

                if (filter.Country != null)
                {
                    predicateBuilder = predicateBuilder.And(address => EF.Functions.Like(address.Country, $"{filter.Country}%"));
                }

                query = query.Where(predicateBuilder);
            }

            var addresses = await query
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
