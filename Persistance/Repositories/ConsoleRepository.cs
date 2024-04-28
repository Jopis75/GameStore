using Application.Interfaces.Persistance;
using Domain.Filters;
using Microsoft.EntityFrameworkCore;
using Persistance.DbContexts;
using System.Linq.Expressions;
using Console = Domain.Entities.Console;

namespace Persistance.Repositories
{
    public class ConsoleRepository : RepositoryBase<Console, ConsoleFilter>, IConsoleRepository
    {
        public ConsoleRepository(GameStoreDbContext gameStoreDbContext)
            : base(gameStoreDbContext) { }

        protected override Task<IEnumerable<Console>> ReadByFilterAsync(ConsoleFilter filter, IQueryable<Console> query, Expression<Func<Console, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Console>> ReadByNameAsync(string name, bool asNoTracking = false)
        {
            var query = Entities.AsQueryable();

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            var consoles = await query
                .Where(console => EF.Functions.Like(console.Name, $"{name}%"))
                .ToListAsync();

            return consoles;
        }
    }
}
