using Abp.Linq.Expressions;
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

        protected override async Task<IEnumerable<Console>> ReadByFilterAsync(ConsoleFilter filter, IQueryable<Console> query, Expression<Func<Console, bool>> predicate)
        {
            if (filter.DeveloperId != null)
            {
                predicate = predicate.And(console => console.DeveloperId == filter.DeveloperId);
            }

            if (filter.ImageUri!= null)
            {
                predicate = predicate.And(console => console.ImageUri != null && EF.Functions.Like(console.ImageUri, $"{filter.ImageUri}%"));
            }

            if (filter.Name != null)
            {
                predicate = predicate.And(console => EF.Functions.Like(console.Name, $"{filter.Name}%"));
            }

            if (filter.Price != null)
            {
                predicate = predicate.And(console => console.Price == filter.Price);
            }

            if (filter.PurchaseDate != null)
            {
                predicate = predicate.And(console => console.PurchaseDate.Date == filter.PurchaseDate.Value.Date);
            }

            if (filter.ReleaseDate != null)
            {
                predicate = predicate.And(console => console.ReleaseDate.Date == filter.ReleaseDate.Value.Date);
            }

            if (filter.Url != null)
            {
                predicate = predicate.And(console => console.Url != null && EF.Functions.Like(console.Url, $"{filter.Url}%"));
            }

            var consoles = await query
                .Where(predicate)
                .ToListAsync();

            return consoles;
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
