﻿using Domain.Entities;

namespace Application.Interfaces.Persistance
{
    public interface IRepositoryBase<TEntity>
        where TEntity : EntityBase, new()
    {
        Task<TEntity> CreateAsync(TEntity entity);

        Task<TEntity> DeleteAsync(TEntity entity);

        Task<bool> ExistsAsync(int id);

        Task<IEnumerable<TEntity>> ReadAllAsync(bool asNoTracking = false);

        Task<TEntity> ReadByIdAsync(int id, bool asNoTracking = false);

        Task<TEntity> UpdateAsync(TEntity entity);
    }
}
