﻿namespace Application.Interfaces.Persistance
{
    public interface IUnitOfWork : IDisposable
    {
        IAddressRepository AddressRepository { get; }

        ICompanyRepository CompanyRepository { get; }

        IConsoleRepository ConsoleRepository { get; }

        IConsoleVideoGameRepository ConsoleVideoGameRepository { get; }

        IGenreRepository GenreRepository { get; }

        IReviewRepository ReviewRepository { get; }

        Task SaveAsync();

        ITrophyRepository TrophyRepository { get; }

        IVideoGameGenreRepository VideoGameGenreRepository { get; }

        IVideoGameRepository VideoGameRepository { get; }
    }
}
