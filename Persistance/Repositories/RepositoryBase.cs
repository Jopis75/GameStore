﻿using Abp.Linq.Expressions;
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
            get { return _gameStoreDbContext; }
        }

        protected DbSet<TEntity> Entities
        {
            get { return _entities; }
        }

        protected IMapper Mapper
        {
            get { return _mapper; }
        }

        public RepositoryBase(GameStoreDbContext gameStoreDbContext, IMapper mapper)
        {
            _gameStoreDbContext = gameStoreDbContext;
            _entities = _gameStoreDbContext.Set<TEntity>();
            _mapper = mapper;
        }

        public async Task<TDto> CreateAsync(TDto dto, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<TEntity>(dto);
            EntityEntry<TEntity> entityEntry = await _gameStoreDbContext.AddAsync<TEntity>(entity, cancellationToken);
            await _gameStoreDbContext.SaveChangesAsync();

            return _mapper.Map<TDto>(entityEntry.Entity);
        }

        public async Task<TDto> DeleteAsync(TDto dto, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<TEntity>(dto);

            cancellationToken.ThrowIfCancellationRequested();

            EntityEntry<TEntity> entityEntry = _gameStoreDbContext.Remove<TEntity>(entity);
            await _gameStoreDbContext.SaveChangesAsync();

            return _mapper.Map<TDto>(entityEntry.Entity);
        }

        public async Task<TDto> DeleteByIdAsync(int id, CancellationToken cancellationToken)
        {
            var entity = await Entities
                .Where(entity => entity.Id == id)
                .SingleAsync(cancellationToken);

            cancellationToken.ThrowIfCancellationRequested();

            var entityEntry = _gameStoreDbContext.Remove<TEntity>(entity);
            await _gameStoreDbContext.SaveChangesAsync();

            return _mapper.Map<TDto>(entityEntry.Entity);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await Entities.AnyAsync(entity => entity.Id == id);
        }

        public async Task<IEnumerable<TDto>> ReadAllAsync(CancellationToken cancellationToken)
        {
            var entities = await Entities
                .AsNoTracking()
                .ToArrayAsync(cancellationToken);

            return entities.Select(_mapper.Map<TDto>);
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
                return new TDto();
            }

            return _mapper.Map<TDto>(entity);
        }

        public async Task<TDto> UpdateAsync(TDto dto, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<TEntity>(dto);

            cancellationToken.ThrowIfCancellationRequested();

            EntityEntry<TEntity> entityEntry = _gameStoreDbContext.Update<TEntity>(entity);
            await _gameStoreDbContext.SaveChangesAsync();

            return _mapper.Map<TDto>(entityEntry.Entity);
        }
    }
}
