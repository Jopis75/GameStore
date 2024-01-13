using Application.Dtos.Addresses;
using Application.Dtos.Common;
using Application.Dtos.Companies;
using Application.Dtos.Consoles;
using Application.Dtos.ConsoleVideoGames;
using Application.Dtos.Identity;
using Application.Dtos.Reviews;
using Application.Dtos.VideoGames;
using Application.Validators.Addresses;
using Application.Validators.Common;
using Application.Validators.Companies;
using Application.Validators.Consoles;
using Application.Validators.ConsoleVideoGames;
using Application.Validators.Identity;
using Application.Validators.Reviews;
using Application.Validators.VideoGames;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddAutoMapper();
            serviceCollection.AddMediatR();
            serviceCollection.AddValidators();

            return serviceCollection;
        }

        private static IServiceCollection AddAutoMapper(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddAutoMapper(Assembly.GetExecutingAssembly());

            return serviceCollection;
        }

        private static IServiceCollection AddMediatR(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddMediatR(mediatRServiceConfiguration => mediatRServiceConfiguration.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            return serviceCollection;
        }

        private static IServiceCollection AddValidators(this IServiceCollection serviceCollection)
        {
            // Addresses.
            serviceCollection.AddScoped<IValidator<CreateAddressRequestDto>, CreateAddressRequestDtoValidator>();
            serviceCollection.AddScoped<IValidator<UpdateAddressRequestDto>, UpdateAddressRequestDtoValidator>();
            serviceCollection.AddScoped<IValidator<DeleteAddressRequestDto>, DeleteAddressRequestDtoValidator>();

            // Companies.
            serviceCollection.AddScoped<IValidator<CreateCompanyRequestDto>, CreateCompanyRequestDtoValidator>();
            serviceCollection.AddScoped<IValidator<UpdateCompanyRequestDto>, UpdateCompanyRequestDtoValidator>();
            serviceCollection.AddScoped<IValidator<DeleteCompanyRequestDto>, DeleteCompanyRequestDtoValidator>();

            // Common.
            serviceCollection.AddScoped<IValidator<ReadByIdRequestDto>, ReadByIdRequestDtoValidator>();

            // Consoles.
            serviceCollection.AddScoped<IValidator<CreateConsoleRequestDto>, CreateConsoleRequestDtoValidator>();
            serviceCollection.AddScoped<IValidator<UpdateConsoleRequestDto>, UpdateConsoleRequestDtoValidator>();
            serviceCollection.AddScoped<IValidator<DeleteConsoleRequestDto>, DeleteConsoleRequestDtoValidator>();

            // ConsoleVideoGames.
            serviceCollection.AddScoped<IValidator<CreateConsoleVideoGameRequestDto>, CreateConsoleVideoGameRequestDtoValidator>();
            serviceCollection.AddScoped<IValidator<UpdateConsoleVideoGameRequestDto>, UpdateConsoleVideoGameRequestDtoValidator>();
            serviceCollection.AddScoped<IValidator<DeleteConsoleVideoGameRequestDto>, DeleteConsoleVideoGameRequestDtoValidator>();

            // Identity.
            serviceCollection.AddScoped<IValidator<LoginRequestDto>, LoginRequestDtoValidator>();

            // Reviews.
            serviceCollection.AddScoped<IValidator<CreateReviewRequestDto>, CreateReviewRequestDtoValidator>();
            serviceCollection.AddScoped<IValidator<ReadReviewByVideoGameIdRequestDto>, ReadReviewByVideoGameIdRequestDtoValidator>();
            serviceCollection.AddScoped<IValidator<UpdateReviewRequestDto>, UpdateReviewRequestDtoValidator>();
            serviceCollection.AddScoped<IValidator<DeleteReviewRequestDto>, DeleteReviewRequestDtoValidator>();

            // VideoGames.
            serviceCollection.AddScoped<IValidator<CreateVideoGameRequestDto>, CreateVideoGameRequestDtoValidator>();
            serviceCollection.AddScoped<IValidator<UpdateVideoGameRequestDto>, UpdateVideoGameRequestDtoValidator>();
            serviceCollection.AddScoped<IValidator<DeleteVideoGameRequestDto>, DeleteVideoGameRequestDtoValidator>();

            return serviceCollection;
        }
    }
}
