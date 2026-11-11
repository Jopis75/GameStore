using GameStore.Domain.Dtos;
using GameStore.Domain.Entities;
using GameStore.Domain.Filters;

namespace GameStore.Application.Interfaces.Persistance
{
    public interface IVideoGameGenreRepository : IRepositoryBase<VideoGameGenre, VideoGameGenreDto, VideoGameGenreFilter>
    {
        Task<IEnumerable<VideoGameGenreDto>> ReadByVideoGameIdAsync(int videoGameId, CancellationToken cancellationToken);
    }
}
