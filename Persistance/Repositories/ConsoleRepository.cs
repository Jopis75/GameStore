﻿using Application.Interfaces.Persistance;
using Microsoft.EntityFrameworkCore;
using Persistance.DbContexts;
using Console = Domain.Entities.Console;

namespace Persistance.Repositories
{
    public class ConsoleRepository : RepositoryBase<Console>, IConsoleRepository
    {
        public ConsoleRepository(GameStoreDbContext gameStoreDbContext)
            : base(gameStoreDbContext)
        { }

        public async Task<Console> ReadByNameAsync(string name, bool asNoTracking = false)
        {
            var console = asNoTracking ?
                await Entities
                    .AsNoTracking()
                    .Where(console => console.Name == name)
                    .SingleOrDefaultAsync() :
                await Entities
                    .Where(console => console.Name == name)
                    .SingleOrDefaultAsync();

            if (console == null)
            {
                return new Console();
            }

            return console;
        }
    }
}
