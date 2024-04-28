using Abp.Linq.Expressions;
using Application.Interfaces.Persistance;
using Domain.Entities;
using Domain.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Persistance.DbContexts;
using System.Linq.Expressions;

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

        public async Task<IEnumerable<TEntity>> ReadByFilterAsync(TFilter filter, bool asNoTracking = false)
        {
            var query = Entities.AsQueryable();

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            var predicate = PredicateBuilder.New<TEntity>();

            if (filter.Id != null)
            {
                predicate = predicate.And(entity => entity.Id == filter.Id);
            }

            if (filter.CreatedAt != null)
            {
                predicate = predicate.And(entity => entity.CreatedAt != null && entity.CreatedAt.Value.Date == filter.CreatedAt.Value.Date);
            }

            if (filter.CreatedBy != null)
            {
                predicate = predicate.And(entity => entity.CreatedBy != null && entity.CreatedBy == filter.CreatedBy);
            }

            if (filter.UpdatedAt != null)
            {
                predicate = predicate.And(entity => entity.UpdatedAt != null && entity.UpdatedAt.Value.Date == filter.UpdatedAt.Value.Date);
            }

            if (filter.UpdatedBy != null)
            {
                predicate = predicate.And(entity => entity.UpdatedBy != null && entity.UpdatedBy == filter.UpdatedBy);
            }

            if (filter.DeletedAt != null)
            {
                predicate = predicate.And(entity => entity.DeletedAt != null && entity.DeletedAt.Value.Date == filter.DeletedAt.Value.Date);
            }

            if (filter.DeletedBy != null)
            {
                predicate = predicate.And(entity => entity.DeletedBy != null && entity.DeletedBy == filter.DeletedBy);
            }

            return await ReadByFilterAsync(filter, query, predicate);
        }

        protected abstract Task<IEnumerable<TEntity>> ReadByFilterAsync(TFilter filter, IQueryable<TEntity> query, Expression<Func<TEntity, bool>> predicate);

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

        public Task<TEntity> UpdateAsync(TEntity entity)
        {
            EntityEntry<TEntity> entityEntry = _gameStoreDbContext.Update<TEntity>(entity);
            return Task.FromResult(entityEntry.Entity);
        }
    }
}
