using Abp.Linq.Expressions;
using AutoMapper;
using GameStore.Application.Interfaces.Persistance;
using GameStore.Domain.Dtos;
using GameStore.Domain.Entities;
using GameStore.Domain.Filters;
using GameStore.Persistance.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GameStore.Persistance.Repositories
{
    public class GenreRepository(GameStoreDbContextFactory gameStoreDbContextFactory, IMapper mapper) : RepositoryBase<Genre, GenreDto, GenreFilter>(gameStoreDbContextFactory, mapper), IGenreRepository
    {
        protected override async Task<IEnumerable<GenreDto>> ReadByFilterAsync(GenreFilter filter, Expression<Func<Genre, bool>> predicate, CancellationToken cancellationToken)
        {
            if (filter.Name != null)
            {
                predicate = predicate.And(genre => genre.Name != null && EF.Functions.Like(genre.Name, $"{filter.Name}%"));
            }

            if (filter.Description != null)
            {
                predicate = predicate.And(genre => genre.Description != null && EF.Functions.Like(genre.Description, $"{filter.Description}%"));
            }

            using var gameStoreDbContext = gameStoreDbContextFactory.CreateGameStoreDbContext();

            var genres = await gameStoreDbContext
                .Set<Genre>()
                .AsNoTracking()
                .Where(predicate)
                .ToArrayAsync(cancellationToken);

            return genres.Select(mapper.Map<GenreDto>);
        }

        public async Task<IEnumerable<GenreDto>> ReadByNameAsync(string name, CancellationToken cancellationToken)
        {
            using var gameStoreDbContext = gameStoreDbContextFactory.CreateGameStoreDbContext();

            var genres = await gameStoreDbContext
                .Set<Genre>()
                .AsNoTracking()
                .Where(genre => EF.Functions.Like(genre.Name, $"{name}%"))
                .ToArrayAsync(cancellationToken);

            return genres.Select(mapper.Map<GenreDto>);
        }

        public async Task<GenreDto> ReadByNameExactAsync(string name, CancellationToken cancellationToken)
        {
            using var gameStoreDbContext = gameStoreDbContextFactory.CreateGameStoreDbContext();

            var genre = await gameStoreDbContext
                .Set<Genre>()
                .AsNoTracking()
                .Where(genre => genre.Name == name)
                .SingleOrDefaultAsync(cancellationToken);

            if (genre == null)
            {
                return NullObject;
            }

            return mapper.Map<GenreDto>(genre);
        }
    }
}
