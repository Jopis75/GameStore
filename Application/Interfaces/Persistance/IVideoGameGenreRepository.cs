using Domain.Dtos;
using Domain.Entities;
using Domain.Filters;

namespace Application.Interfaces.Persistance
{
    public interface IVideoGameGenreRepository : IRepositoryBase<VideoGameGenre, VideoGameGenreDto, VideoGameGenreFilter>
    {
        Task<IEnumerable<VideoGameGenreDto>> ReadByVideoGameIdAsync(int videoGameId, CancellationToken cancellationToken);
    }
}
