using Abp.Linq.Expressions;
using Application.Interfaces.Persistance;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Persistance.DbContexts;
using System.Linq.Expressions;

namespace Persistance.Repositories
{
    public abstract class RepositoryBase<TEntity, TDto, TFilter>(GameStoreDbContextFactory gameStoreDbContextFactory, IMapper mapper) : IRepositoryBase<TEntity, TDto, TFilter>
        where TEntity : EntityBase, new()
        where TDto : DtoBase, new()
        where TFilter : FilterBase, new()
    {
        protected IMapper Mapper => mapper;

        public TDto NullObject => new();

        public async Task<TDto> CreateAsync(TDto dto, CancellationToken cancellationToken)
        {
            try
            {
                var entity = Mapper.Map<TEntity>(dto);

                using var gameStoreDbContext = gameStoreDbContextFactory.CreateGameStoreDbContext();

                EntityEntry<TEntity> entityEntry = await gameStoreDbContext.AddAsync<TEntity>(entity, cancellationToken);
                await gameStoreDbContext.SaveChangesAsync();

                return Mapper.Map<TDto>(entityEntry.Entity);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<TDto> DeleteAsync(TDto dto, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<TEntity>(dto);

            cancellationToken.ThrowIfCancellationRequested();

            using var gameStoreDbContext = gameStoreDbContextFactory.CreateGameStoreDbContext();

            EntityEntry<TEntity> entityEntry = gameStoreDbContext.Remove<TEntity>(entity);
            await gameStoreDbContext.SaveChangesAsync();

            return Mapper.Map<TDto>(entityEntry.Entity);
        }

        public async Task<TDto> DeleteByIdAsync(int id, CancellationToken cancellationToken)
        {
            using var gameStoreDbContext = gameStoreDbContextFactory.CreateGameStoreDbContext();

            var entity = await gameStoreDbContext
                .Set<TEntity>()
                .Where(entity => entity.Id == id)
                .SingleAsync(cancellationToken);

            cancellationToken.ThrowIfCancellationRequested();

            var entityEntry = gameStoreDbContext.Remove<TEntity>(entity);
            await gameStoreDbContext.SaveChangesAsync();

            return Mapper.Map<TDto>(entityEntry.Entity);
        }

        public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken)
        {
            using var gameStoreDbContext = gameStoreDbContextFactory.CreateGameStoreDbContext();

            return await gameStoreDbContext
                .Set<TEntity>()
                .AnyAsync(entity => entity.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<TDto>> ReadAllAsync(CancellationToken cancellationToken)
        {
            using var gameStoreDbContext = gameStoreDbContextFactory.CreateGameStoreDbContext();

            var entities = await gameStoreDbContext
                .Set<TEntity>()
                .AsNoTracking()
                .ToArrayAsync(cancellationToken);

            return entities.Select(Mapper.Map<TDto>);
        }

        public async Task<IEnumerable<TDto>> ReadByFilterAsync(TFilter filter, CancellationToken cancellationToken)
        {
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

            return await ReadByFilterAsync(filter, predicate, cancellationToken);
        }

        protected abstract Task<IEnumerable<TDto>> ReadByFilterAsync(TFilter filter, Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken);

        public async Task<TDto> ReadByIdAsync(int id, CancellationToken cancellationToken)
        {
            using var gameStoreDbContext = gameStoreDbContextFactory.CreateGameStoreDbContext();

            var entity = await gameStoreDbContext
                .Set<TEntity>()
                .Where(entity => entity.Id == id)
                .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                return NullObject;
            }

            return Mapper.Map<TDto>(entity);
        }

        public async Task<TDto> UpdateAsync(TDto dto, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<TEntity>(dto);

            cancellationToken.ThrowIfCancellationRequested();

            using var gameStoreDbContext = gameStoreDbContextFactory.CreateGameStoreDbContext();

            EntityEntry<TEntity> entityEntry = gameStoreDbContext.Update<TEntity>(entity);
            await gameStoreDbContext.SaveChangesAsync();

            return Mapper.Map<TDto>(entityEntry.Entity);
        }
    }
}
