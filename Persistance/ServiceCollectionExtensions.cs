﻿using Application.Interfaces.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistance.DbContexts;
using Persistance.Repositories;

namespace Persistance
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPersistanceServices(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddDbContext(configuration);
            serviceCollection.AddRepositories();
            serviceCollection.AddUnitOfWork();

            return serviceCollection;
        }

        private static IServiceCollection AddDbContext(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddDbContext<GameStoreDbContext>(optionsBuilder =>
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("GameStoreConnectionString")));

            return serviceCollection;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IAddressRepository, AddressRepository>();
            serviceCollection.AddScoped<ICompanyRepository, CompanyRepository>();
            serviceCollection.AddScoped<IConsoleVideoGameRepository, ConsoleVideoGameRepository>();
            serviceCollection.AddScoped<IConsoleRepository, ConsoleRepository>();
            serviceCollection.AddScoped<IGenreRepository, GenreRepository>();
            serviceCollection.AddScoped<IReviewRepository, ReviewRepository>();
            serviceCollection.AddScoped<ITrophyRepository, TrophyRepository>();
            serviceCollection.AddScoped<IVideoGameGenreRepository, VideoGameGenreRepository>();
            serviceCollection.AddScoped<IVideoGameRepository, VideoGameRepository>();

            return serviceCollection;
        }

        private static IServiceCollection AddUnitOfWork(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();

            return serviceCollection;
        }
    }
}
