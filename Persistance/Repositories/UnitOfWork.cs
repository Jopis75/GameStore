using Application.Interfaces.Persistance;
using Microsoft.EntityFrameworkCore.Storage;
using Persistance.DbContexts;

namespace Persistance.Repositories
{
    public class UnitOfWork(
        GameStoreDbContext gameStoreDbContext,
        IAddressRepository addressRepository,
        ICompanyRepository companyRepository,
        IConsoleRepository consoleRepository,
        IConsoleVideoGameRepository consoleVideoGameRepository,
        IGenreRepository genreRepository,
        IReviewRepository reviewRepository,
        ITrophyRepository trophyRepository,
        IVideoGameGenreRepository videoGameGenreRepository,
        IVideoGameRepository videoGameRepository) : IUnitOfWork
    {
        private IDbContextTransaction _dbContextTransaction = default!;

        public IAddressRepository AddressRepository => addressRepository;

        public ICompanyRepository CompanyRepository => companyRepository;

        public IConsoleRepository ConsoleRepository => consoleRepository;

        public IConsoleVideoGameRepository ConsoleVideoGameRepository => consoleVideoGameRepository;

        public IGenreRepository GenreRepository => genreRepository;

        public IReviewRepository ReviewRepository => reviewRepository;

        public ITrophyRepository TrophyRepository => trophyRepository;

        public IVideoGameGenreRepository VideoGameGenreRepository => videoGameGenreRepository;

        public IVideoGameRepository VideoGameRepository => videoGameRepository;

        public async Task BeginTransactionAsync(CancellationToken cancellationToken)
        {
            _dbContextTransaction = await gameStoreDbContext.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken)
        {
            try
            {
                await gameStoreDbContext.SaveChangesAsync(cancellationToken);

                await _dbContextTransaction.CommitAsync(cancellationToken);
            }
            catch (Exception)
            {
                await _dbContextTransaction.RollbackAsync(cancellationToken);

                throw;
            }
        }

        public void Dispose()
        {
            gameStoreDbContext.Dispose();

            GC.SuppressFinalize(this);
        }

        public async Task RollbackTransactionAsync(CancellationToken cancellationToken)
        {
            await _dbContextTransaction.RollbackAsync(cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await gameStoreDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
