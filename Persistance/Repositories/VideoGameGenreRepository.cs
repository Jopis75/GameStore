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
                .ToArrayAsync(cancellationToken);

            return videoGameGenres.Select(Mapper.Map<VideoGameGenreDto>);
        }

        public async Task<IEnumerable<VideoGameGenreDto>> ReadByVideoGameIdAsync(int videoGameId, CancellationToken cancellationToken)
        {
            var videoGameGenres = await Entities
                .AsNoTracking()
                .Where(videoGameGenre => videoGameGenre.VideoGameId == videoGameId)
                .ToArrayAsync(cancellationToken);

            return videoGameGenres.Select(Mapper.Map<VideoGameGenreDto>);
        }
    }
}
