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

        public async Task<IEnumerable<Company>> GetByFoundedAsync(DateTime founded)
        {
            var companies = await Entities
                .Where(entity => entity.Founded == founded)
                .ToListAsync();

            return companies;
        }

        public async Task<Company> GetByNameAsync(string name)
        {
            var company = await Entities
                .Where(entity => entity.Name == name)
                .SingleOrDefaultAsync();

            if (company == null)
            {
                return new Company();
            }

            return company;
        }

        public async Task<IEnumerable<Company>> GetByNumberOfEmployeesAsync(int numberOfEmployees)
        {
            var companies = await Entities
                .Where(entity => entity.NumberOfEmployees == numberOfEmployees)
                .ToListAsync();

            return companies;
        }

        public async Task<IEnumerable<Company>> GetByNumberOfEmployeesAsync(int fromNumberOfEmployees, int toNumberOfEmployees)
        {
            var companies = await Entities
                .Where(entity => entity.NumberOfEmployees >= fromNumberOfEmployees && entity.NumberOfEmployees <= toNumberOfEmployees)
                .ToListAsync();

            return companies;
        }

        public async Task<Company> GetByTradeNameAsync(string tradeName)
        {
            var company = await Entities
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
