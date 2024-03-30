using Application.Interfaces.Persistance;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistance.DbContexts;

namespace Persistance.Repositories
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(GameStoreDbContext gameStoreDbContext)
            : base(gameStoreDbContext)
        { }

        public async Task<Company> ReadByEmailAddressAsync(string emailAddress, bool asNoTracking = false)
        {
            var company = asNoTracking ?
                await Entities
                    .AsNoTracking<Company>()
                    .Where(company => company.EmailAddress == emailAddress)
                    .SingleOrDefaultAsync() :
                await Entities
                    .Where(company => company.EmailAddress == emailAddress)
                    .SingleOrDefaultAsync();

            if (company == null)
            {
                return new Company();
            }

            return company;
        }

        public async Task<Company> ReadByNameAsync(string name, bool asNoTracking = false)
        {
            var company = asNoTracking ?
                await Entities
                    .AsNoTracking<Company>()
                    .Where(company => company.Name == name)
                    .SingleOrDefaultAsync() :
                await Entities
                    .Where(company => company.Name == name)
                    .SingleOrDefaultAsync();

            if (company == null)
            {
                return new Company();
            }

            return company;
        }

        public async Task<Company> ReadByPhoneNumberAsync(string phoneNumber, bool asNoTracking = false)
        {
            var company = asNoTracking ?
                await Entities
                    .AsNoTracking<Company>()
                    .Where(company => company.PhoneNumber == phoneNumber)
                    .SingleOrDefaultAsync() :
                await Entities
                    .Where(company => company.PhoneNumber == phoneNumber)
                    .SingleOrDefaultAsync();

            if (company == null)
            {
                return new Company();
            }

            return company;
        }

        public async Task<Company> ReadByTradeNameAsync(string tradeName, bool asNoTracking = false)
        {
            var company = asNoTracking ?
                await Entities
                    .AsNoTracking<Company>()
                    .Where(company => company.TradeName == tradeName)
                    .SingleOrDefaultAsync() :
                await Entities
                    .Where(company => company.TradeName == tradeName)
                    .SingleOrDefaultAsync();

            if (company == null)
            {
                return new Company();
            }

            return company;
        }
    }
}
