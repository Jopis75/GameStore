namespace Application.Interfaces.Persistance
{
    public interface IUnitOfWork : IDisposable
    {
        IAddressRepository AddressRepository { get; }

        ICompanyRepository CompanyRepository { get; }

        IConsoleRepository ConsoleRepository { get; }

        IConsoleVideoGameRepository ConsoleVideoGameRepository { get; }

        IGenreRepository GenreRepository { get; }

        IReviewRepository ReviewRepository { get; }

        ITrophyRepository TrophyRepository { get; }

        IVideoGameGenreRepository VideoGameGenreRepository { get; }

        IVideoGameRepository VideoGameRepository { get; }

        Task BeginTransactionAsync(CancellationToken cancellationToken);

        Task CommitTransactionAsync(CancellationToken cancellationToken);

        Task RollbackTransactionAsync(CancellationToken cancellationToken);

        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
