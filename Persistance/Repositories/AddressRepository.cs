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

        public async Task<IEnumerable<AddressDto>> ReadByCityAsync(string city, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var addresses = await Entities
                .AsNoTracking()
                .Where(address => EF.Functions.Like(address.City, $"{city}%"))
                .ToArrayAsync();

            return addresses.Select(Mapper.Map<AddressDto>);
        }

        protected override async Task<IEnumerable<AddressDto>> ReadByFilterAsync(AddressFilter filter, Expression<Func<Address, bool>> predicate, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

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

            var addresses = await Entities
                .AsNoTracking()
                .Where(predicate)
                .ToArrayAsync();

            return addresses.Select(Mapper.Map<AddressDto>);
        }

        public async Task<IEnumerable<AddressDto>> ReadByStreetAddressAsync(string streetAddress, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var addresses = await Entities
                .AsNoTracking()
                .Where(address => address.StreetAddress == streetAddress)
                .ToArrayAsync();

            return addresses.Select(Mapper.Map<AddressDto>);
        }

        public async Task<IEnumerable<AddressDto>> ReadByPostalCodeAsync(string postalCode, CancellationToken cancellationToken)
        {
           cancellationToken.ThrowIfCancellationRequested();

            var addresses = await Entities
                .AsNoTracking()
                .Where(address => EF.Functions.Like(address.PostalCode, $"{postalCode}%"))
                .ToArrayAsync();

            return addresses.Select(Mapper.Map<AddressDto>);
        }
    }
}
