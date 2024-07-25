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
    public class GenreRepository : RepositoryBase<Genre, GenreDto, GenreFilter>, IGenreRepository
    {
        public GenreRepository(GameStoreDbContext gameStoreDbContext, IMapper mapper)
            : base(gameStoreDbContext, mapper)
        {
        }

        protected override async Task<IEnumerable<GenreDto>> ReadByFilterAsync(GenreFilter filter, Expression<Func<Genre, bool>> predicate, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (filter.Name != null)
            {
                predicate = predicate.And(genre => genre.Name != null && EF.Functions.Like(genre.Name, $"{filter.Name}%"));
            }

            if (filter.Description != null)
            {
                predicate = predicate.And(genre => genre.Description != null && EF.Functions.Like(genre.Description, $"{filter.Description}%"));
            }

            var genres = await Entities
                .AsNoTracking()
                .Where(predicate)
                .ToArrayAsync();

            return genres.Select(Mapper.Map<GenreDto>);
        }

        public async Task<IEnumerable<GenreDto>> ReadByNameAsync(string name, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var genres = await Entities
                .AsNoTracking()
                .Where(genre => EF.Functions.Like(genre.Name, $"{name}%"))
                .ToArrayAsync();

            return genres.Select(Mapper.Map<GenreDto>);
        }
    }
}
