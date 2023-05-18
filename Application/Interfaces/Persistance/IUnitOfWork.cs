namespace Application.Interfaces.Persistance
{
    public interface IUnitOfWork : IDisposable
    {
        IAddressRepository AddressRepository { get; }

        ICompanyRepository CompanyRepository { get; }

        IProductRepository ProductRepository { get; }

        IReviewRepository ReviewRepository { get; }

        Task SaveAsync();
    }
}
