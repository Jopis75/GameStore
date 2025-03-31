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
    public abstract class RepositoryBase<TEntity, TDto, TFilter>(GameStoreDbContext gameStoreDbContext, IMapper mapper) : IRepositoryBase<TEntity, TDto, TFilter>
        where TEntity : EntityBase, new()
        where TDto : DtoBase, new()
        where TFilter : FilterBase, new()
    {
        protected GameStoreDbContext DbContext => gameStoreDbContext;

        protected DbSet<TEntity> Entities => gameStoreDbContext.Set<TEntity>();

        protected IMapper Mapper => mapper;

        public TDto NullObject => new();

        public async Task<TDto> CreateAsync(TDto dto, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<TEntity>(dto);

            EntityEntry<TEntity> entityEntry = await DbContext.AddAsync<TEntity>(entity, cancellationToken);
            await DbContext.SaveChangesAsync();

            return Mapper.Map<TDto>(entityEntry.Entity);
        }

        public async Task<TDto> DeleteAsync(TDto dto, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<TEntity>(dto);

            cancellationToken.ThrowIfCancellationRequested();

            EntityEntry<TEntity> entityEntry = DbContext.Remove<TEntity>(entity);
            await DbContext.SaveChangesAsync();

            return Mapper.Map<TDto>(entityEntry.Entity);
        }

        public async Task<TDto> DeleteByIdAsync(int id, CancellationToken cancellationToken)
        {
            var entity = await Entities
                .Where(entity => entity.Id == id)
                .SingleAsync(cancellationToken);

            cancellationToken.ThrowIfCancellationRequested();

            var entityEntry = DbContext.Remove<TEntity>(entity);
            await DbContext.SaveChangesAsync();

            return Mapper.Map<TDto>(entityEntry.Entity);
        }

        public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken)
        {
            return await Entities.AnyAsync(entity => entity.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<TDto>> ReadAllAsync(CancellationToken cancellationToken)
        {
            var entities = await Entities
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
            var entity = await Entities
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

            EntityEntry<TEntity> entityEntry = DbContext.Update<TEntity>(entity);
            await DbContext.SaveChangesAsync();

            return Mapper.Map<TDto>(entityEntry.Entity);
        }
    }
}
