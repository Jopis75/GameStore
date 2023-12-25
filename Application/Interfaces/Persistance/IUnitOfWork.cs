namespace Application.Interfaces.Persistance
{
    public interface IUnitOfWork : IDisposable
    {
        IAddressRepository AddressRepository { get; }

        ICompanyRepository CompanyRepository { get; }

        IConsoleRepository ConsoleRepository { get; }

        IConsoleVideoGameRepository ConsoleVideoGameRepository { get; }

        IReviewRepository ReviewRepository { get; }

        Task SaveAsync();

        IVideoGameRepository VideoGameRepository { get; }
    }
}
