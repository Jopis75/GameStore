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
        Task<TDto> CreateAsync(TDto dto);

        Task<TDto> DeleteAsync(TDto dto);

        Task<TDto> DeleteByIdAsync(int id);

        Task<bool> ExistsAsync(int id);

        Task<IEnumerable<TDto>> ReadAllAsync(bool asNoTracking = false);

        Task<TDto> ReadByIdAsync(int id, bool asNoTracking = false);

        Task<IEnumerable<TDto>> ReadByFilterAsync(TFilter filter, bool asNoTracking = false);

        Task<TDto> UpdateAsync(TDto dto);
    }
}
