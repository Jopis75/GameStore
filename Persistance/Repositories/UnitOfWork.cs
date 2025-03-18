using Application.Interfaces.Persistance;
using Persistance.DbContexts;

namespace Persistance.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GameStoreDbContext _gameStoreDbContext;

        //private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly IAddressRepository _addressRepository;

        private readonly ICompanyRepository _companyRepository;

        private readonly IConsoleRepository _consoleRepository;

        private readonly IConsoleVideoGameRepository _consoleVideoGameRepository;

        private readonly IGenreRepository _genreRepository;

        private readonly IReviewRepository _reviewRepository;

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
            /*IHttpContextAccessor httpContextAccessor, */
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
            //_httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
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

        public void Dispose()
        {
            _gameStoreDbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task SaveAsync()
        {
            //var userName = _httpContextAccessor.HttpContext.User.FindFirst(String.Empty)?.Value;
            await _gameStoreDbContext.SaveChangesAsync();
        }
    }
}
