using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistance.Configurations;
using Console = Domain.Entities.Console;

namespace Persistance.DbContexts
{
    public class GameStoreDbContext : DbContext
    {
        public DbSet<Address> Addresses { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<ConsoleVideoGame> ConsoleVideoGames { get; set; }

        public DbSet<Console> Consoles { get; set; }

        public DbSet<VideoGame> VideoGames { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public GameStoreDbContext(DbContextOptions<GameStoreDbContext> dbContextOptions) 
            : base(dbContextOptions)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Address.
            modelBuilder
                .Entity<Address>()
                .ToTable("Address");
            modelBuilder
                .Entity<Address>()
                .HasKey(address => address.Id);

            // Company.
            modelBuilder
                .Entity<Company>()
                .ToTable("Company");
            modelBuilder
                .Entity<Company>()
                .HasKey(company => company.Id);
            modelBuilder
                .Entity<Company>()
                .HasMany(company => company.VideoGames)
                .WithOne(videoGame => videoGame.Developer)
                .HasForeignKey(videoGame => videoGame.DeveloperId);
            modelBuilder
                .Entity<Company>()
                .HasOne(company => company.Headquarter)
                .WithOne(address => address.Company)
                .HasForeignKey<Company>(company => company.HeadquarterId);

            // Console.
            modelBuilder
                .Entity<Console>()
                .ToTable("Console");
            modelBuilder
                .Entity<Console>()
                .HasKey(console => console.Id);
            modelBuilder
                .Entity<Console>()
                .HasOne(console => console.Developer)
                .WithMany(company => company.Consoles)
                .HasForeignKey(console => console.DeveloperId);
            modelBuilder
                .Entity<Console>()
                .HasMany(console => console.Reviews)
                .WithOne(review => review.Console)
                .HasForeignKey(review => review.ConsoleId);
            modelBuilder
                .Entity<Console>()
                .Property(console => console.Price)
                .HasPrecision(18, 2);

            // VideoGame.
            modelBuilder
                .Entity<VideoGame>()
                .ToTable("VideoGame");
            modelBuilder
                .Entity<VideoGame>()
                .HasKey(videoGame => videoGame.Id);
            modelBuilder
                .Entity<VideoGame>()
                .HasOne(videoGame => videoGame.Developer)
                .WithMany(company => company.VideoGames)
                .HasForeignKey(videoGame => videoGame.DeveloperId);
            modelBuilder
                .Entity<VideoGame>()
                .HasMany(videoGame => videoGame.Reviews)
                .WithOne(review => review.VideoGame)
                .HasForeignKey(review => review.VideoGameId);
            modelBuilder
                .Entity<VideoGame>()
                .Property(videoGame => videoGame.Price)
                .HasPrecision(18, 2);

            // ConsoleVideoGame.
            modelBuilder
                .Entity<ConsoleVideoGame>()
                .ToTable("ConsoleVideoGame");
            modelBuilder
                .Entity<ConsoleVideoGame>()
                .HasKey(consoleVideoGame => consoleVideoGame.Id);
            modelBuilder
                .Entity<ConsoleVideoGame>()
                .HasOne(consoleVideoGame => consoleVideoGame.Console)
                .WithMany(console => console.ConsoleVideoGames)
                .HasForeignKey(consoleVideoGame => consoleVideoGame.ConsoleId)
                .OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder
                .Entity<ConsoleVideoGame>()
                .HasOne(consoleVideoGame => consoleVideoGame.VideoGame)
                .WithMany(videoGame => videoGame.ConsoleVideoGames)
                .HasForeignKey(consoleVideoGame => consoleVideoGame.VideoGameId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            // Review.
            modelBuilder
                .Entity<Review>()
                .ToTable("Review");
            modelBuilder
                .Entity<Review>()
                .HasKey(review => review.Id);
            modelBuilder
                .Entity<Review>()
                .HasOne(review => review.VideoGame)
                .WithMany(videoGame => videoGame.Reviews)
                .HasForeignKey(review => review.VideoGameId);

            // Configurations.
            modelBuilder.ApplyConfiguration(new AddressConfiguration());
            modelBuilder.ApplyConfiguration(new ConsoleConfiguration());
            modelBuilder.ApplyConfiguration(new VideoGameConfiguration());
            modelBuilder.ApplyConfiguration(new ConsoleVideoGameConfiguration());
            modelBuilder.ApplyConfiguration(new CompanyConfiguration());
            modelBuilder.ApplyConfiguration(new ReviewConfiguration());
        }

        public virtual async Task<int> SaveChangesAsync(string userName = "System")
        {
            var entries = base
                .ChangeTracker
                .Entries<EntityBase>()
                .Where(entry => entry.State == EntityState.Added || entry.State == EntityState.Modified || entry.State == EntityState.Deleted);

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.Now;
                    entry.Entity.CreatedBy = userName;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = DateTime.Now;
                    entry.Entity.UpdatedBy = userName;
                }
                else if (entry.State == EntityState.Deleted)
                {
                    entry.Entity.DeletedAt = DateTime.Now;
                    entry.Entity.DeletedBy = userName;
                }
            }

            return await base.SaveChangesAsync();
        }
    }
}
