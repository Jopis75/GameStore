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
    public class ConsoleVideoGameRepository : RepositoryBase<ConsoleVideoGame, ConsoleVideoGameDto, ConsoleVideoGameFilter>, IConsoleVideoGameRepository
    {
        public ConsoleVideoGameRepository(GameStoreDbContext gameStoreDbContext, IMapper mapper)
            : base(gameStoreDbContext, mapper)
        {
        }

        protected override async Task<IEnumerable<ConsoleVideoGameDto>> ReadByFilterAsync(ConsoleVideoGameFilter filter, Expression<Func<ConsoleVideoGame, bool>> predicate, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (filter.ConsoleId != null)
            {
                predicate = predicate.And(consoleVideoGame => consoleVideoGame.ConsoleId == filter.ConsoleId);
            }

            if (filter.VideoGameId != null)
            {
                predicate = predicate.And(consoleVideoGame => consoleVideoGame.VideoGameId == filter.VideoGameId);
            }

            var consoleVideoGames = await Entities
                .AsNoTracking()
                .Where(predicate)
                .ToArrayAsync();

            return consoleVideoGames.Select(Mapper.Map<ConsoleVideoGameDto>);
        }

        public async Task<IEnumerable<ConsoleVideoGameDto>> ReadByConsoleIdAsync(int consoleId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var consoleVideoGames = await Entities
                .AsNoTracking()
                .Where(consoleVideoGame => consoleVideoGame.ConsoleId == consoleId)
                .ToArrayAsync();

            return consoleVideoGames.Select(Mapper.Map<ConsoleVideoGameDto>);
        }

        public async Task<IEnumerable<ConsoleVideoGameDto>> ReadByVideoGameIdAsync(int videoGameId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var consoleVideoGames = await Entities
                .AsNoTracking()
                .Where(consoleVideoGame => consoleVideoGame.VideoGameId == videoGameId)
                .ToArrayAsync();

            return consoleVideoGames.Select(Mapper.Map<ConsoleVideoGameDto>);
        }
    }
}
