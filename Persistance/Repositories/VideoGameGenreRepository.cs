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
    public class VideoGameGenreRepository(GameStoreDbContextFactory gameStoreDbContextFactory, IMapper mapper) : RepositoryBase<VideoGameGenre, VideoGameGenreDto, VideoGameGenreFilter>(gameStoreDbContextFactory, mapper), IVideoGameGenreRepository
    {
        protected override async Task<IEnumerable<VideoGameGenreDto>> ReadByFilterAsync(VideoGameGenreFilter filter, Expression<Func<VideoGameGenre, bool>> predicate, CancellationToken cancellationToken)
        {
            if (filter.VideoGameId != null)
            {
                predicate = predicate.And(videoGameGenre => videoGameGenre.VideoGameId == filter.VideoGameId);
            }

            if (filter.GenreId != null)
            {
                predicate = predicate.And(videoGameGenre => videoGameGenre.GenreId == filter.GenreId);
            }

            using var gameStoreDbContext = gameStoreDbContextFactory.CreateGameStoreDbContext();

            var videoGameGenres = await gameStoreDbContext
                .Set<VideoGameGenre>()
                .AsNoTracking()
                .Where(predicate)
                .ToArrayAsync(cancellationToken);

            return videoGameGenres.Select(Mapper.Map<VideoGameGenreDto>);
        }

        public async Task<IEnumerable<VideoGameGenreDto>> ReadByVideoGameIdAsync(int videoGameId, CancellationToken cancellationToken)
        {
            using var gameStoreDbContext = gameStoreDbContextFactory.CreateGameStoreDbContext();

            var videoGameGenres = await gameStoreDbContext
                .Set<VideoGameGenre>()
                .AsNoTracking()
                .Where(videoGameGenre => videoGameGenre.VideoGameId == videoGameId)
                .ToArrayAsync(cancellationToken);

            return videoGameGenres.Select(Mapper.Map<VideoGameGenreDto>);
        }
    }
}
