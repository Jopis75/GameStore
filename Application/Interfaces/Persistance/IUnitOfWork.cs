namespace Application.Interfaces.Persistance
{
    public interface IUnitOfWork : IDisposable
    {
        IAddressRepository AddressRepository { get; }

        ICompanyRepository CompanyRepository { get; }

        IConsoleProductRepository ConsoleProductRepository { get; }

        IConsoleRepository ConsoleRepository { get; }

        IProductRepository ProductRepository { get; }

        IReviewRepository ReviewRepository { get; }

        Task SaveAsync();
    }
}
