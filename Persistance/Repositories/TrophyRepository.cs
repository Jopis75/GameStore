using Abp.Linq.Expressions;
using Application.Interfaces.Persistance;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Enums;
using Domain.Filters;
using Microsoft.EntityFrameworkCore;
using Persistance.DbContexts;
using System.Linq.Expressions;

namespace Persistance.Repositories
{
    public class TrophyRepository : RepositoryBase<Trophy, TrophyDto, TrophyFilter>, ITrophyRepository
    {
        public TrophyRepository(GameStoreDbContext gameStoreDbContext, IMapper mapper)
            : base(gameStoreDbContext, mapper)
        {
        }

        protected override async Task<IEnumerable<TrophyDto>> ReadByFilterAsync(TrophyFilter filter, Expression<Func<Trophy, bool>> predicate, CancellationToken cancellationToken)
        {
            if (filter.Name != null)
            {
                predicate = predicate.And(trophy => EF.Functions.Like(trophy.Name, $"{filter.Name}%"));
            }

            if (filter.Description != null)
            {
                predicate = predicate.And(trophy => EF.Functions.Like(trophy.Description, $"{filter.Description}%"));
            }

            if (filter.TrophyValue != null)
            {
                predicate = predicate.And(trophy => trophy.TrophyValue == filter.TrophyValue);
            }

            if (filter.VideoGameId != null)
            {
                predicate = predicate.And(trophy => trophy.VideoGameId == filter.VideoGameId);
            }

            var trophies = await Entities
                .AsNoTracking()
                .Where(predicate)
                .ToArrayAsync(cancellationToken);

            return trophies.Select(Mapper.Map<TrophyDto>);
        }

        public async Task<IEnumerable<TrophyDto>> ReadByNameAsync(string name, CancellationToken cancellationToken)
        {
            var trophies = await Entities
                .AsNoTracking()
                .Where(trophy => EF.Functions.Like(trophy.Name, $"{name}%"))
                .ToArrayAsync(cancellationToken);

            return trophies.Select(Mapper.Map<TrophyDto>);
        }

        public async Task<IEnumerable<TrophyDto>> ReadByTrophyValueAsync(TrophyValue trophyValue, CancellationToken cancellationToken)
        {
            var trophies = await Entities
                .AsNoTracking()
                .Where(trophy => trophy.TrophyValue == trophyValue)
                .ToArrayAsync(cancellationToken);

            return trophies.Select(Mapper.Map<TrophyDto>);
        }
    }
}
