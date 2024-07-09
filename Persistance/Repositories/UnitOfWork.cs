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

        private readonly IReviewRepository _reviewRepository;

        private readonly IVideoGameRepository _videoGameRepository;

        public IAddressRepository AddressRepository => _addressRepository;

        public ICompanyRepository CompanyRepository => _companyRepository;

        public IConsoleRepository ConsoleRepository => _consoleRepository;

        public IConsoleVideoGameRepository ConsoleVideoGameRepository => _consoleVideoGameRepository;

        public IReviewRepository ReviewRepository => _reviewRepository;

        public IVideoGameRepository VideoGameRepository => _videoGameRepository;

        public UnitOfWork(
            GameStoreDbContext gameStoreDbContext,
            /*IHttpContextAccessor httpContextAccessor, */
            IAddressRepository addressRepository,
            ICompanyRepository companyRepository,
            IConsoleRepository consoleRepository,
            IConsoleVideoGameRepository consoleVideoGameRepository,
            IReviewRepository reviewRepository,
            IVideoGameRepository videoGameRepository)
        {
            _gameStoreDbContext = gameStoreDbContext ?? throw new ArgumentNullException(nameof(gameStoreDbContext));
            //_httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _addressRepository = addressRepository ?? throw new ArgumentNullException(nameof(addressRepository));
            _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
            _consoleRepository = consoleRepository ?? throw new ArgumentNullException(nameof(consoleRepository));
            _consoleVideoGameRepository = consoleVideoGameRepository ?? throw new ArgumentNullException(nameof(consoleVideoGameRepository));
            _reviewRepository = reviewRepository ?? throw new ArgumentNullException(nameof(reviewRepository));
            _videoGameRepository = videoGameRepository ?? throw new ArgumentNullException(nameof(videoGameRepository));
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
