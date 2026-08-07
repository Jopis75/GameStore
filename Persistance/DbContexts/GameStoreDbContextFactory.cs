using Microsoft.EntityFrameworkCore;

namespace Persistance.DbContexts
{
    public class GameStoreDbContextFactory(Action<DbContextOptionsBuilder> dbContextOptionBuilder)
    {
        public GameStoreDbContext CreateGameStoreDbContext()
        {
            var gameStoreDbContextOptionsBuilder = new DbContextOptionsBuilder<GameStoreDbContext>();
            dbContextOptionBuilder(gameStoreDbContextOptionsBuilder);

            return new GameStoreDbContext(gameStoreDbContextOptionsBuilder.Options);
        }
    }
}
