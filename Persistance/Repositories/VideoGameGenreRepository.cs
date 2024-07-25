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
    public class VideoGameGenreRepository : RepositoryBase<VideoGameGenre, VideoGameGenreDto, VideoGameGenreFilter>, IVideoGameGenreRepository
    {
        public VideoGameGenreRepository(GameStoreDbContext gameStoreDbContext, IMapper mapper)
            : base(gameStoreDbContext, mapper)
        {
        }

        protected override async Task<IEnumerable<VideoGameGenreDto>> ReadByFilterAsync(VideoGameGenreFilter filter, Expression<Func<VideoGameGenre, bool>> predicate, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (filter.VideoGameId != null)
            {
                predicate = predicate.And(videoGameGenre => videoGameGenre.VideoGameId == filter.VideoGameId);
            }

            if (filter.GenreId != null)
            {
                predicate = predicate.And(videoGameGenre => videoGameGenre.GenreId == filter.GenreId);
            }

            var videoGameGenres = await Entities
                .AsNoTracking()
                .Where(predicate)
                .ToArrayAsync();

            return videoGameGenres.Select(Mapper.Map<VideoGameGenreDto>);
        }
    }
}
