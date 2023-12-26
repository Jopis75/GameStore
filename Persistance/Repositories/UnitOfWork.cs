using Application.Interfaces.Persistance;
using Microsoft.AspNetCore.Http;
using Persistance.DbContexts;

namespace Persistance.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GameStoreDbContext _gameStoreDbContext;

        private IAddressRepository? _addressRepository;

        private ICompanyRepository? _companyRepository;

        private IConsoleRepository? _consoleRepository;

        private IConsoleVideoGameRepository? _consoleVideoGameRepository;

        //private readonly IHttpContextAccessor _httpContextAccessor;

        private IReviewRepository? _reviewRepository;

        private IVideoGameRepository? _videoGameRepository;

        public IAddressRepository AddressRepository =>
            _addressRepository ??= new AddressRepository(_gameStoreDbContext);

        public ICompanyRepository CompanyRepository =>
            _companyRepository ??= new CompanyRepository(_gameStoreDbContext);

        public IConsoleRepository ConsoleRepository =>
            _consoleRepository ??= new ConsoleRepository(_gameStoreDbContext);

        public IConsoleVideoGameRepository ConsoleVideoGameRepository =>
            _consoleVideoGameRepository ??= new ConsoleVideoGameRepository(_gameStoreDbContext);

        public IReviewRepository ReviewRepository =>
            _reviewRepository ??= new ReviewRepository(_gameStoreDbContext);

        public IVideoGameRepository VideoGameRepository =>
            _videoGameRepository ??= new VideoGameRepository(_gameStoreDbContext);

        public UnitOfWork(GameStoreDbContext gameStoreDbContext/*, IHttpContextAccessor httpContextAccessor*/)
        {
            _gameStoreDbContext = gameStoreDbContext ?? throw new ArgumentNullException(nameof(gameStoreDbContext));
            //_httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public void Dispose()
        {
            _gameStoreDbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task SaveAsync()
        {
            //var userName = _httpContextAccessor.HttpContext.User.FindFirst(string.Empty)?.Value;
            await _gameStoreDbContext.SaveChangesAsync(/*userName ??*/ "System");
        }
    }
}
