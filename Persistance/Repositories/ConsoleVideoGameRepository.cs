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

        protected override async Task<IEnumerable<ConsoleVideoGameDto>> ReadByFilterAsync(ConsoleVideoGameFilter filter, IQueryable<ConsoleVideoGame> query, Expression<Func<ConsoleVideoGame, bool>> predicate)
        {
            if (filter.ConsoleId != null)
            {
                predicate = predicate.And(consoleVideoGame => consoleVideoGame.ConsoleId == filter.ConsoleId);
            }

            if (filter.VideoGameId != null)
            {
                predicate = predicate.And(consoleVideoGame => consoleVideoGame.VideoGameId == filter.VideoGameId);
            }

            var consoleVideoGames = await query
                .Where(predicate)
                .ToListAsync();

            return consoleVideoGames.Select(Mapper.Map<ConsoleVideoGameDto>);
        }

        public async Task<IEnumerable<ConsoleVideoGameDto>> ReadByConsoleIdAsync(int consoleId, bool asNoTracking = false)
        {
            var query = Entities.AsQueryable();

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            var consoleVideoGames = await query
                .Where(consoleVideoGame => consoleVideoGame.ConsoleId == consoleId)
                .ToListAsync();

            return consoleVideoGames.Select(Mapper.Map<ConsoleVideoGameDto>);
        }

        public async Task<IEnumerable<ConsoleVideoGameDto>> ReadByVideoGameIdAsync(int videoGameId, bool asNoTracking = false)
        {
            var query = Entities.AsQueryable();

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            var consoleVideoGames = await query
                .Where(consoleVideoGame => consoleVideoGame.VideoGameId == videoGameId)
                .ToListAsync();

            return consoleVideoGames.Select(Mapper.Map<ConsoleVideoGameDto>);
        }
    }
}
