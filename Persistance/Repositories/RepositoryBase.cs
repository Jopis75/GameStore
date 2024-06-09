using Abp.Linq.Expressions;
using Application.Interfaces.Persistance;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Domain.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Internal;
using Persistance.DbContexts;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Persistance.Repositories
{
    public abstract class RepositoryBase<TEntity, TDto, TFilter> : IRepositoryBase<TEntity, TDto, TFilter>
        where TEntity : EntityBase, new()
        where TDto : DtoBase, new()
        where TFilter : FilterBase, new()
    {
        private readonly GameStoreDbContext _gameStoreDbContext;

        private readonly DbSet<TEntity> _entities;

        private readonly IMapper _mapper;

        protected GameStoreDbContext DbContext
        {
            get
            {
                return _gameStoreDbContext;
            }
        }

        protected DbSet<TEntity> Entities
        {
            get
            {
                return _entities;
            }
        }

        protected IMapper Mapper
        {
            get
            {
                return _mapper;
            }
        }

        public RepositoryBase(GameStoreDbContext gameStoreDbContext, IMapper mapper)
        {
            _gameStoreDbContext = gameStoreDbContext ?? throw new ArgumentNullException(nameof(gameStoreDbContext));
            _entities = _gameStoreDbContext.Set<TEntity>();
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<TDto> CreateAsync(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            EntityEntry<TEntity> entityEntry = await _gameStoreDbContext.AddAsync<TEntity>(entity);
            return _mapper.Map<TDto>(entityEntry.Entity);
        }

        public Task<TDto> DeleteAsync(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            EntityEntry<TEntity> entityEntry = _gameStoreDbContext.Remove<TEntity>(entity);
            return Task.FromResult<TDto>(_mapper.Map<TDto>(entityEntry.Entity));
        }

        public async Task<TDto> DeleteByIdAsync(int id)
        {
            var query = Entities.AsQueryable();

            var entity = await query
                .Where(entity => entity.Id == id)
                .SingleAsync();

            var entityEntry = _gameStoreDbContext.Remove<TEntity>(entity);
            return _mapper.Map<TDto>(entityEntry.Entity);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await Entities.AnyAsync(entity => entity.Id == id);
        }

        public async Task<IEnumerable<TDto>> ReadAllAsync(bool asNoTracking = false)
        {
            var query = _entities.AsQueryable();

            if (asNoTracking)
            {
                query = query.AsNoTracking();
            }

            var entities = await query.ToListAsync();

            return entities.Select(_mapper.Map<TDto>);
        }

        public async Task<IEnumerable<TDto>> ReadByFilterAsync(TFilter filter, bool asNoTracking = false)
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

        protected abstract Task<IEnumerable<TDto>> ReadByFilterAsync(TFilter filter, IQueryable<TEntity> query, Expression<Func<TEntity, bool>> predicate);

        public async Task<TDto> ReadByIdAsync(int id, bool asNoTracking = false)
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
                return new TDto();
            }

            return _mapper.Map<TDto>(entity);
        }

        public Task<TDto> UpdateAsync(TDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);
            EntityEntry<TEntity> entityEntry = _gameStoreDbContext.Update<TEntity>(entity);
            return Task.FromResult(_mapper.Map<TDto>(entityEntry.Entity));
        }
    }
}
