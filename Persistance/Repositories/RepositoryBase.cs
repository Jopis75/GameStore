using Application.Interfaces.Persistance;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Persistance.DbContexts;

namespace Persistance.Repositories
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity>
        where TEntity : EntityBase, new()
    {
        private readonly GameStoreDbContext _gameStoreDbContext;

        private readonly DbSet<TEntity> _entities;

        public GameStoreDbContext Dbcontext
        {
            get { return _gameStoreDbContext; }
        }

        public DbSet<TEntity> Entities
        {
            get { return _entities; }
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

        public async Task<IEnumerable<TEntity>> ReadAllAsync()
        {
            return await Entities.ToListAsync();
        }

        public async Task<TEntity> ReadByIdAsync(int id)
        {
            var entity = await Entities
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
