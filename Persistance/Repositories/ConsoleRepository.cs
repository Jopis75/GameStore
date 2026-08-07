using Abp.Linq.Expressions;
using Application.Interfaces.Persistance;
using AutoMapper;
using Domain.Dtos;
using Domain.Filters;
using Microsoft.EntityFrameworkCore;
using Persistance.DbContexts;
using System.Linq.Expressions;
using Console = Domain.Entities.Console;

namespace Persistance.Repositories
{
    public class ConsoleRepository(GameStoreDbContextFactory gameStoreDbContextFactory, IMapper mapper) : RepositoryBase<Console, ConsoleDto, ConsoleFilter>(gameStoreDbContextFactory, mapper), IConsoleRepository
    {
        protected override async Task<IEnumerable<ConsoleDto>> ReadByFilterAsync(ConsoleFilter filter, Expression<Func<Console, bool>> predicate, CancellationToken cancellationToken)
        {
            if (filter.DeveloperId != null)
            {
                predicate = predicate.And(console => console.DeveloperId == filter.DeveloperId);
            }

            if (filter.ImageUri != null)
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

            using var gameStoreDbContext = gameStoreDbContextFactory.CreateGameStoreDbContext();

            var consoles = await gameStoreDbContext
                .Set<Console>()
                .AsNoTracking()
                .Where(predicate)
                .ToArrayAsync(cancellationToken);

            return consoles.Select(Mapper.Map<ConsoleDto>);
        }

        public async Task<IEnumerable<ConsoleDto>> ReadByNameAsync(string name, CancellationToken cancellationToken)
        {
            using var gameStoreDbContext = gameStoreDbContextFactory.CreateGameStoreDbContext();

            var consoles = await gameStoreDbContext
                .Set<Console>()
                .AsNoTracking()
                .Where(console => EF.Functions.Like(console.Name, $"{name}%"))
                .ToArrayAsync(cancellationToken);

            return consoles.Select(Mapper.Map<ConsoleDto>);
        }

        public async Task<ConsoleDto> ReadByNameExactAsync(string name, CancellationToken cancellationToken)
        {
            using var gameStoreDbContext = gameStoreDbContextFactory.CreateGameStoreDbContext();

            var console = await gameStoreDbContext
                .Set<Console>()
                .AsNoTracking()
                .Where(console => console.Name == name)
                .SingleOrDefaultAsync(cancellationToken);

            if (console == null)
            {
                return NullObject;
            }

            return Mapper.Map<ConsoleDto>(console);
        }
    }
}
