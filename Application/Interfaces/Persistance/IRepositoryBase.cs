using Domain.Entities;
using Domain.Filters;

namespace Application.Interfaces.Persistance
{
    public interface IRepositoryBase<TEntity, TFilter>
        where TEntity : EntityBase, new()
        where TFilter : FilterBase, new()
    {
        Task<TEntity> CreateAsync(TEntity entity);

        Task<TEntity> DeleteAsync(TEntity entity);

        Task<bool> ExistsAsync(int id);

        Task<IEnumerable<TEntity>> ReadAllAsync(bool asNoTracking = false);

        Task<TEntity> ReadByIdAsync(int id, bool asNoTracking = false);

        Task<IEnumerable<TEntity>> ReadByFilterAsync(TFilter filter, bool asNoTracking = false);

        Task<TEntity> UpdateAsync(TEntity entity);
    }
}
