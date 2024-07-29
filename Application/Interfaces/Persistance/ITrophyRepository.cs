using Domain.Dtos;
using Domain.Entities;
using Domain.Enums;
using Domain.Filters;

namespace Application.Interfaces.Persistance
{
    public interface ITrophyRepository : IRepositoryBase<Trophy, TrophyDto, TrophyFilter>
    {
        Task<IEnumerable<TrophyDto>> ReadByNameAsync(string name, CancellationToken cancellationToken);

        Task<IEnumerable<TrophyDto>> ReadByTrophyValueAsync(TrophyValue trophyValue, CancellationToken cancellationToken);
    }
}
