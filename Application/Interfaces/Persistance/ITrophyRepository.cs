using GameStore.Domain.Dtos;
using GameStore.Domain.Entities;
using GameStore.Domain.Enums;
using GameStore.Domain.Filters;

namespace GameStore.Application.Interfaces.Persistance
{
    public interface ITrophyRepository : IRepositoryBase<Trophy, TrophyDto, TrophyFilter>
    {
        Task<IEnumerable<TrophyDto>> ReadByNameAsync(string name, CancellationToken cancellationToken);

        Task<IEnumerable<TrophyDto>> ReadByTrophyValueAsync(TrophyValue trophyValue, CancellationToken cancellationToken);
    }
}
