﻿using Abp.Linq.Expressions;
using Application.Interfaces.Persistance;
using AutoMapper;
using Domain.Dtos;
using Domain.Filters;
using Microsoft.EntityFrameworkCore;
using Persistance.DbContexts;
using System.Linq.Expressions;
using Console = Domain.Entities.Console;

namespace Persistance.Repositories
{
    public class ConsoleRepository : RepositoryBase<Console, ConsoleDto, ConsoleFilter>, IConsoleRepository
    {
        public ConsoleRepository(GameStoreDbContext gameStoreDbContext, IMapper mapper)
            : base(gameStoreDbContext, mapper)
        {
        }

        protected override async Task<IEnumerable<ConsoleDto>> ReadByFilterAsync(ConsoleFilter filter, Expression<Func<Console, bool>> predicate, CancellationToken cancellationToken)
        {
            if (filter.DeveloperId != null)
            {
                predicate = predicate.And(console => console.DeveloperId == filter.DeveloperId);
            }

            if (filter.ImageUri != null)
            {
                predicate = predicate.And(console => console.ImageUri != null && EF.Functions.Like(console.ImageUri, $"{filter.ImageUri}%"));
            }

            if (filter.Name != null)
            {
                predicate = predicate.And(console => EF.Functions.Like(console.Name, $"{filter.Name}%"));
            }

            if (filter.Price != null)
            {
                predicate = predicate.And(console => console.Price == filter.Price);
            }

            if (filter.PurchaseDate != null)
            {
                predicate = predicate.And(console => console.PurchaseDate.Date == filter.PurchaseDate.Value.Date);
            }

            if (filter.ReleaseDate != null)
            {
                predicate = predicate.And(console => console.ReleaseDate.Date == filter.ReleaseDate.Value.Date);
            }

            if (filter.Url != null)
            {
                predicate = predicate.And(console => console.Url != null && EF.Functions.Like(console.Url, $"{filter.Url}%"));
            }

            var consoles = await Entities
                .AsNoTracking()
                .Where(predicate)
                .ToArrayAsync(cancellationToken);

            return consoles.Select(Mapper.Map<ConsoleDto>);
        }

        public async Task<IEnumerable<ConsoleDto>> ReadByNameAsync(string name, CancellationToken cancellationToken)
        {
            var consoles = await Entities
                .AsNoTracking()
                .Where(console => EF.Functions.Like(console.Name, $"{name}%"))
                .ToArrayAsync(cancellationToken);

            return consoles.Select(Mapper.Map<ConsoleDto>);
        }
    }
}
