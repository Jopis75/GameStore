using Abp.Linq.Expressions;
using Application.Interfaces.Persistance;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Filters;
using Microsoft.EntityFrameworkCore;
using Persistance.DbContexts;
using System.Linq.Expressions;

namespace Persistance.Repositories
{
    public class AddressRepository : RepositoryBase<Address, AddressDto, AddressFilter>, IAddressRepository
    {
        public AddressRepository(GameStoreDbContext gameStoreDbContext, IMapper mapper)
            : base(gameStoreDbContext, mapper)
        {
        }

        public async Task<IEnumerable<AddressDto>> ReadByCityAsync(string city, bool asNoTracking = false)
        {
            var query = Entities.AsQueryable();

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            var addresses = await query
                .Where(address => EF.Functions.Like(address.City, $"{city}%"))
                .ToListAsync();

            return addresses.Select(Mapper.Map<AddressDto>);
        }

        protected override async Task<IEnumerable<AddressDto>> ReadByFilterAsync(AddressFilter filter, IQueryable<Address> query, Expression<Func<Address, bool>> predicate)
        {
            if (filter.City != null)
            {
                predicate = predicate.And(address => EF.Functions.Like(address.City, $"{filter.City}%"));
            }

            if (filter.Country != null)
            {
                predicate = predicate.And(address => EF.Functions.Like(address.Country, $"{filter.Country}%"));
            }

            if (filter.PostalCode != null)
            {
                predicate = predicate.And(address => EF.Functions.Like(address.PostalCode, $"{filter.PostalCode}%"));
            }

            if (filter.State != null)
            {
                predicate = predicate.And(address => EF.Functions.Like(address.State, $"{filter.State}%"));
            }

            if (filter.StreetAddress != null)
            {
                predicate = predicate.And(address => EF.Functions.Like(address.StreetAddress, $"{filter.StreetAddress}%"));
            }

            var addresses = await query
                .Where(predicate)
                .ToListAsync();

            return addresses.Select(Mapper.Map<AddressDto>);
        }

        public async Task<IEnumerable<AddressDto>> ReadByStreetAddressAsync(string streetAddress, bool asNoTracking = false)
        {
            var query = Entities.AsQueryable();

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            var addresses = await query
                .Where(address => EF.Functions.Like(address.StreetAddress, $"{streetAddress}%"))
                .ToListAsync();

            return addresses.Select(Mapper.Map<AddressDto>);
        }

        public async Task<IEnumerable<AddressDto>> ReadByPostalCodeAsync(string postalCode, bool asNoTracking = false)
        {
            var query = Entities.AsQueryable();

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            var addresses = await query
                .Where(address => EF.Functions.Like(address.PostalCode, $"{postalCode}%"))
                .ToListAsync();

            return addresses.Select(Mapper.Map<AddressDto>);
        }
    }
}
