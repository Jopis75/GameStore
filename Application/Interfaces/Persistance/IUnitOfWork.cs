namespace Application.Interfaces.Persistance
{
    public interface IUnitOfWork : IDisposable
    {
        IAddressRepository AddressRepository { get; }

        ICompanyRepository CompanyRepository { get; }

        IConsoleRepository ConsoleRepository { get; }

        IProductRepository ProductRepository { get; }

        IReviewRepository ReviewRepository { get; }

        Task SaveAsync();
    }
}
