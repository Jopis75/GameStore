using Application.Interfaces.Persistance;
using Domain.Entities;
using Domain.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Persistance.DbContexts;

namespace Persistance.Repositories
{
    public abstract class RepositoryBase<TEntity, TFilter> : IRepositoryBase<TEntity, TFilter>
        where TEntity : EntityBase, new()
        where TFilter : FilterBase, new()
    {
        private readonly GameStoreDbContext _gameStoreDbContext;

        private readonly DbSet<TEntity> _entities;

        public GameStoreDbContext DbContext
        {
            get
            {
                return _gameStoreDbContext;
            }
        }

        public DbSet<TEntity> Entities
        {
            get
            {
                return _entities;
            }
        }

        public RepositoryBase(GameStoreDbContext gameStoreDbContext)
        {
            _gameStoreDbContext = gameStoreDbContext ?? throw new ArgumentNullException(nameof(gameStoreDbContext));
            _entities = _gameStoreDbContext.Set<TEntity>();
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            EntityEntry<TEntity> entityEntry = await _gameStoreDbContext.AddAsync<TEntity>(entity);
            return entityEntry.Entity;
        }

        public Task<TEntity> DeleteAsync(TEntity entity)
        {
            EntityEntry<TEntity> entityEntry = _gameStoreDbContext.Remove<TEntity>(entity);
            return Task.FromResult<TEntity>(entityEntry.Entity);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await Entities.AnyAsync(entity => entity.Id == id);
        }

        public async Task<IEnumerable<TEntity>> ReadAllAsync(bool asNoTracking = false)
        {
            var query = _entities.AsQueryable();

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            var entities = await query
                .ToListAsync();

            return entities;
        }

        public async Task<TEntity> ReadByIdAsync(int id, bool asNoTracking = false)
        {
            var query = _entities.AsQueryable();

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            var entity = await query
                .Where(entity => entity.Id == id)
                .SingleOrDefaultAsync();

            if (entity == null)
            {
                return new TEntity();
            }

            return entity;
        }

        public abstract Task<IEnumerable<TEntity>> ReadByFilterAsync(TFilter filter, bool asNoTracking = false);

        public Task<TEntity> UpdateAsync(TEntity entity)
        {
            EntityEntry<TEntity> entityEntry = _gameStoreDbContext.Update<TEntity>(entity);
            return Task.FromResult(entityEntry.Entity);
        }
    }
}
