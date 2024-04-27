using Application.Interfaces.Persistance;
using Domain.Entities;
using Domain.Filters;
using Microsoft.EntityFrameworkCore;
using Persistance.DbContexts;
using System.Formats.Asn1;

namespace Persistance.Repositories
{
    public class CompanyRepository : RepositoryBase<Company, CompanyFilter>, ICompanyRepository
    {
        public CompanyRepository(GameStoreDbContext gameStoreDbContext)
            : base(gameStoreDbContext) { }

        public async Task<IEnumerable<Company>> ReadByEmailAddressAsync(string emailAddress, bool asNoTracking = false)
        {
            var query = Entities.AsQueryable();

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            var companies = await query
                .Where(company => EF.Functions.Like(company.EmailAddress, $"{emailAddress}%"))
                .ToListAsync();

            return companies;
        }

        public override Task<IEnumerable<Company>> ReadByFilterAsync(CompanyFilter filter, bool asNoTracking = false)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Company>> ReadByNameAsync(string name, bool asNoTracking = false)
        {
            var query = Entities.AsQueryable();

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            var companies = await query
                .Where(company => EF.Functions.Like(company.Name, $"{name}%"))
                .ToListAsync();

            return companies;
        }

        public async Task<IEnumerable<Company>> ReadByPhoneNumberAsync(string phoneNumber, bool asNoTracking = false)
        {
            var query = Entities.AsQueryable();

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            var companies = await query
                .Where(company => EF.Functions.Like(company.PhoneNumber, $"{phoneNumber}%"))
                .ToListAsync();

            return companies;
        }

        public async Task<IEnumerable<Company>> ReadByTradeNameAsync(string tradeName, bool asNoTracking = false)
        {
            var query = Entities.AsQueryable();

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            var companies = await query
                .Where(company => EF.Functions.Like(company.TradeName, $"{tradeName}%"))
                .ToListAsync();

            return companies;
        }
    }
}
