using Application.Interfaces.Persistance;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistance.DbContexts;

namespace Persistance.Repositories
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(GameStoreDbContext gameStoreDbContext)
            : base(gameStoreDbContext) { }

        public async Task<Company> GetByNameAsync(string name, bool asNoTracking = false)
        {
            var company = asNoTracking ?
                await Entities
                    .AsNoTracking<Company>()
                    .Where(entity => entity.Name == name)
                    .SingleOrDefaultAsync() :
                await Entities
                    .Where(entity => entity.Name == name)
                    .SingleOrDefaultAsync();

            if (company == null)
            {
                return new Company();
            }

            return company;
        }

        public async Task<Company> GetByTradeNameAsync(string tradeName, bool asNoTracking = false)
        {
            var company = asNoTracking ?
                await Entities
                    .AsNoTracking<Company>()
                    .Where(entity => entity.TradeName == tradeName)
                    .SingleOrDefaultAsync() :
                await Entities
                    .Where(entity => entity.TradeName == tradeName)
                    .SingleOrDefaultAsync();

            if (company == null)
            {
                return new Company();
            }

            return company;
        }
    }
}
