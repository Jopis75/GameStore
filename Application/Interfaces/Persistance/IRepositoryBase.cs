using Domain.Dtos;
using Domain.Entities;
using Domain.Filters;

namespace Application.Interfaces.Persistance
{
    public interface IRepositoryBase<TEntity, TDto, TFilter>
        where TEntity : EntityBase, new()
        where TDto : DtoBase, new()
        where TFilter : FilterBase, new()
    {
        Task<TDto> CreateAsync(TDto dto, CancellationToken cancellationToken);

        Task<TDto> DeleteAsync(TDto dto, CancellationToken cancellationToken);

        Task<TDto> DeleteByIdAsync(int id, CancellationToken cancellationToken);

        Task<bool> ExistsAsync(int id);

        Task<IEnumerable<TDto>> ReadAllAsync(CancellationToken cancellationToken);

        Task<TDto> ReadByIdAsync(int id, CancellationToken cancellationToken);

        Task<IEnumerable<TDto>> ReadByFilterAsync(TFilter filter, CancellationToken cancellationToken);

        Task<TDto> UpdateAsync(TDto dto, CancellationToken cancellationToken);
    }
}
