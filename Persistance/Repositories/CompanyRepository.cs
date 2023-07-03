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

        public async Task<IEnumerable<Company>> GetByFoundedAsync(DateTime founded, bool asNoTracking = false)
        {
            var companies = asNoTracking ?
                await Entities
                    .AsNoTracking<Company>()
                    .Where(entity => entity.Founded == founded)
                    .ToListAsync() :
                await Entities
                    .Where(entity => entity.Founded == founded)
                    .ToListAsync();

            return companies;
        }

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

        public async Task<IEnumerable<Company>> GetByNumberOfEmployeesAsync(int numberOfEmployees, bool asNoTracking = false)
        {
            var companies = asNoTracking ?
                await Entities
                    .AsNoTracking<Company>()
                    .Where(entity => entity.NumberOfEmployees == numberOfEmployees)
                    .ToListAsync() :
                await Entities
                    .Where(entity => entity.NumberOfEmployees == numberOfEmployees)
                    .ToListAsync();

            return companies;
        }

        public async Task<IEnumerable<Company>> GetByNumberOfEmployeesAsync(int fromNumberOfEmployees, int toNumberOfEmployees, bool asNoTracking = false)
        {
            var companies = asNoTracking ?
                await Entities
                    .AsNoTracking<Company>()
                    .Where(entity => entity.NumberOfEmployees >= fromNumberOfEmployees && entity.NumberOfEmployees <= toNumberOfEmployees)
                    .ToListAsync() :
                await Entities
                    .Where(entity => entity.NumberOfEmployees >= fromNumberOfEmployees && entity.NumberOfEmployees <= toNumberOfEmployees)
                    .ToListAsync();

            return companies;
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
