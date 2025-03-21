using Application.Interfaces.Persistance;
using Microsoft.EntityFrameworkCore.Storage;
using Persistance.DbContexts;

namespace Persistance.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GameStoreDbContext _gameStoreDbContext;

        private readonly IAddressRepository _addressRepository;

        private readonly ICompanyRepository _companyRepository;

        private readonly IConsoleRepository _consoleRepository;

        private readonly IConsoleVideoGameRepository _consoleVideoGameRepository;

        private readonly IGenreRepository _genreRepository;

        private readonly IReviewRepository _reviewRepository;

        private IDbContextTransaction _transaction = default!;

        private readonly ITrophyRepository _trophyRepository;

        private readonly IVideoGameGenreRepository _videoGameGenreRepository;

        private readonly IVideoGameRepository _videoGameRepository;

        public IAddressRepository AddressRepository => _addressRepository;

        public ICompanyRepository CompanyRepository => _companyRepository;

        public IConsoleRepository ConsoleRepository => _consoleRepository;

        public IConsoleVideoGameRepository ConsoleVideoGameRepository => _consoleVideoGameRepository;

        public IGenreRepository GenreRepository => _genreRepository;

        public IReviewRepository ReviewRepository => _reviewRepository;

        public ITrophyRepository TrophyRepository => _trophyRepository;

        public IVideoGameGenreRepository VideoGameGenreRepository => _videoGameGenreRepository;

        public IVideoGameRepository VideoGameRepository => _videoGameRepository;

        public UnitOfWork(
            GameStoreDbContext gameStoreDbContext,
            IAddressRepository addressRepository,
            ICompanyRepository companyRepository,
            IConsoleRepository consoleRepository,
            IConsoleVideoGameRepository consoleVideoGameRepository,
            IGenreRepository genreRepository,
            IReviewRepository reviewRepository,
            ITrophyRepository trophyRepository,
            IVideoGameGenreRepository videoGameGenreRepository,
            IVideoGameRepository videoGameRepository)
        {
            _gameStoreDbContext = gameStoreDbContext;
            _addressRepository = addressRepository;
            _companyRepository = companyRepository;
            _consoleRepository = consoleRepository;
            _consoleVideoGameRepository = consoleVideoGameRepository;
            _genreRepository = genreRepository;
            _reviewRepository = reviewRepository;
            _trophyRepository = trophyRepository;
            _videoGameGenreRepository = videoGameGenreRepository;
            _videoGameRepository = videoGameRepository;
        }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken)
        {
            _transaction = await _gameStoreDbContext.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken)
        {
            try
            {
                await _gameStoreDbContext.SaveChangesAsync(cancellationToken);

                await _transaction.CommitAsync(cancellationToken);
            }
            catch (Exception)
            {
                await _transaction.RollbackAsync(cancellationToken);

                throw;
            }
        }

        public void Dispose()
        {
            _gameStoreDbContext.Dispose();

            GC.SuppressFinalize(this);
        }

        public async Task RollbackTransactionAsync(CancellationToken cancellationToken)
        {
            await _transaction.RollbackAsync(cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _gameStoreDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
