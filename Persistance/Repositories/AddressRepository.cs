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
            var addresses = await Entities
                .AsNoTracking()
                .Where(address => address.City == city)
                .ToArrayAsync(cancellationToken);

            return addresses.Select(Mapper.Map<AddressDto>);
        }

        protected override async Task<IEnumerable<AddressDto>> ReadByFilterAsync(AddressFilter filter, Expression<Func<Address, bool>> predicate, CancellationToken cancellationToken)
        {
            if (filter.City != null)
            {
                predicate = predicate.And(address => address.City == filter.City);
            }

            if (filter.Country != null)
            {
                predicate = predicate.And(address => address.Country == filter.Country);
            }

            if (filter.PostalCode != null)
            {
                predicate = predicate.And(address => address.PostalCode == filter.PostalCode);
            }

            if (filter.State != null)
            {
                predicate = predicate.And(address => address.State == filter.State);
            }

            if (filter.StreetAddress != null)
            {
                predicate = predicate.And(address => address.StreetAddress == filter.StreetAddress);
            }

            var addresses = await Entities
                .AsNoTracking()
                .Where(predicate)
                .ToArrayAsync(cancellationToken);

            return addresses.Select(Mapper.Map<AddressDto>);
        }

        public async Task<IEnumerable<AddressDto>> ReadByStreetAddressAsync(string streetAddress, CancellationToken cancellationToken)
        {
            var addresses = await Entities
                .AsNoTracking()
                .Where(address => address.StreetAddress == streetAddress)
                .ToArrayAsync(cancellationToken);

            return addresses.Select(Mapper.Map<AddressDto>);
        }

        public async Task<IEnumerable<AddressDto>> ReadByPostalCodeAsync(string postalCode, CancellationToken cancellationToken)
        {
           var addresses = await Entities
                .AsNoTracking()
                .Where(address => address.PostalCode == postalCode)
                .ToArrayAsync(cancellationToken);

            return addresses.Select(Mapper.Map<AddressDto>);
        }
    }
}
