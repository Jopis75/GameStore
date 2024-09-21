using Application.Dtos.Identity;
using Application.Features.Addresses.Requests.Commands;
using Application.Features.Addresses.Requests.Queries;
using Application.Features.Companies.Requests.Commands;
using Application.Features.Companies.Requests.Queries;
using Application.Features.Consoles.Requests.Commands;
using Application.Features.Consoles.Requests.Queries;
using Application.Features.ConsoleVideoGames.Requests.Commands;
using Application.Features.ConsoleVideoGames.Requests.Queries;
using Application.Features.Genres.Requests.Commands;
using Application.Features.Genres.Requests.Queries;
using Application.Features.Reviews.Requests.Commands;
using Application.Features.Reviews.Requests.Queries;
using Application.Features.Trophies.Requests.Commands;
using Application.Features.Trophies.Requests.Queries;
using Application.Features.VideoGames.Requests.Commands;
using Application.Features.VideoGames.Requests.Queries;
using Application.Profiles;
using Application.Validators.Dtos;
using Application.Validators.Identity;
using Application.Validators.Requests.Addresses.Commands;
using Application.Validators.Requests.Addresses.Queries;
using Application.Validators.Requests.Companies.Commands;
using Application.Validators.Requests.Companies.Queries;
using Application.Validators.Requests.Consoles.Commands;
using Application.Validators.Requests.Consoles.Queries;
using Application.Validators.Requests.ConsoleVideoGame.Commands;
using Application.Validators.Requests.ConsoleVideoGame.Queries;
using Application.Validators.Requests.Genres.Commands;
using Application.Validators.Requests.Genres.Queries;
using Application.Validators.Requests.Reviews.Commands;
using Application.Validators.Requests.Reviews.Queries;
using Application.Validators.Requests.Trophies.Commands;
using Application.Validators.Requests.Trophies.Queries;
using Application.Validators.Requests.VideoGames.Commands;
using Application.Validators.Requests.VideoGames.Queries;
using Domain.Dtos;
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
            serviceCollection.AddAutoMapper(typeof(GameStoreProfile));

            return serviceCollection;
        }

        private static IServiceCollection AddMediatR(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddMediatR(mediatRServiceConfiguration => mediatRServiceConfiguration.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            return serviceCollection;
        }

        private static IServiceCollection AddValidators(this IServiceCollection serviceCollection)
        {
            // Domain.Dtos.
            serviceCollection.AddScoped<IValidator<AddressDto>, AddressDtoValidator>();
            serviceCollection.AddScoped<IValidator<CompanyDto>, CompanyDtoValidator>();
            serviceCollection.AddScoped<IValidator<ConsoleDto>, ConsoleDtoValidator>();
            serviceCollection.AddScoped<IValidator<ConsoleVideoGameDto>, ConsoleVideoGameDtoValidator>();
            serviceCollection.AddScoped<IValidator<GenreDto>, GenreDtoValidator>();
            serviceCollection.AddScoped<IValidator<ReviewDto>, ReviewDtoValidator>();
            serviceCollection.AddScoped<IValidator<VideoGameGenreDto>, VideoGameGenreDtoValidator>();
            serviceCollection.AddScoped<IValidator<VideoGameDto>, VideoGameDtoValidator>();

            // Application.Dtos.Identity.
            serviceCollection.AddScoped<IValidator<LoginRequestDto>, LoginRequestDtoValidator>();
            serviceCollection.AddScoped<IValidator<RegistrationRequestDto>, RegistrationRequestDtoValidator>();

            // Features.Addresses.Requests.
            serviceCollection.AddScoped<IValidator<CreateAddressRequest>, CreateAddressRequestValidator>();
            serviceCollection.AddScoped<IValidator<DeleteAddressRequest>, DeleteAddressRequestValidator>();
            serviceCollection.AddScoped<IValidator<UpdateAddressRequest>, UpdateAddressRequestValidator>();
            serviceCollection.AddScoped<IValidator<ReadAddressByIdRequest>, ReadAddressByIdRequestValidator>();

            // Features.Companies.Requests.
            serviceCollection.AddScoped<IValidator<CreateCompanyRequest>, CreateCompanyRequestValidator>();
            serviceCollection.AddScoped<IValidator<CreateCompanyWithHeadquarterRequest>, CreateCompanyWithHeadquarterRequestValidator>();
            serviceCollection.AddScoped<IValidator<DeleteCompanyRequest>, DeleteCompanyRequestValidator>();
            serviceCollection.AddScoped<IValidator<UpdateCompanyRequest>, UpdateCompanyRequestValidator>();
            serviceCollection.AddScoped<IValidator<ReadCompanyByIdRequest>, ReadCompanyByIdRequestValidator>();

            // Features.Consoles.Requests.
            serviceCollection.AddScoped<IValidator<CreateConsoleRequest>, CreateConsoleRequestValidator>();
            serviceCollection.AddScoped<IValidator<DeleteConsoleRequest>, DeleteConsoleRequestValidator>();
            serviceCollection.AddScoped<IValidator<UpdateConsoleRequest>, UpdateConsoleRequestValidator>();
            serviceCollection.AddScoped<IValidator<ReadConsoleByIdRequest>, ReadConsoleByIdRequestValidator>();

            // Features.ConsoleVideoGames.Requests.
            serviceCollection.AddScoped<IValidator<Features.ConsoleVideoGames.Requests.Commands.CreateGenreRequest>, CreateConsoleVideoGameRequestValidator>();
            serviceCollection.AddScoped<IValidator<DeleteConsoleVideoGameRequest>, DeleteConsoleVideoGameRequestValidator>();
            serviceCollection.AddScoped<IValidator<UpdateGenresRequest>, UpdateConsoleVideoGameRequestValidator>();
            serviceCollection.AddScoped<IValidator<ReadConsoleVideoGameByIdRequest>, ReadConsoleVideoGameByIdRequestValidator>();

            // Features.Genres.Requests.
            serviceCollection.AddScoped<IValidator<Features.Genres.Requests.Commands.CreateGenreRequest>, CreateGenreRequestValidator>();
            serviceCollection.AddScoped<IValidator<DeleteGenreRequest>, DeleteGenreRequestValidator>();
            serviceCollection.AddScoped<IValidator<UpdateGenreRequest>, UpdateGenreRequestValidator>();
            serviceCollection.AddScoped<IValidator<ReadGenreByIdRequest>, ReadGenreByIdRequestValidator>();

            // Features.Reviews.Requests.
            serviceCollection.AddScoped<IValidator<CreateReviewRequest>, CreateReviewRequestValidator>();
            serviceCollection.AddScoped<IValidator<DeleteReviewRequest>, DeleteReviewRequestValidator>();
            serviceCollection.AddScoped<IValidator<UpdateReviewRequest>, UpdateReviewRequestValidator>();
            serviceCollection.AddScoped<IValidator<ReadReviewByIdRequest>, ReadReviewByIdRequestValidator>();
            serviceCollection.AddScoped<IValidator<ReadReviewsByVideoGameIdRequest>, ReadReviewsByVideoGameIdRequestValidator>();

            // Features.Trophies.Requests.
            serviceCollection.AddScoped<IValidator<CreateTrophyRequest>, CreateTrophyRequestValidator>();
            serviceCollection.AddScoped<IValidator<DeleteTrophyRequest>, DeleteTrophyRequestValidator>();
            serviceCollection.AddScoped<IValidator<UpdateTrophyRequest>, UpdateTrophyRequestValidator>();
            serviceCollection.AddScoped<IValidator<ReadTrophyByIdRequest>, ReadTrophyByIdRequestValidator>();

            // Features.VideoGames.Requests.
            serviceCollection.AddScoped<IValidator<CreateVideoGameRequest>, CreateVideoGameRequestValidator>();
            serviceCollection.AddScoped<IValidator<DeleteVideoGameRequest>, DeleteVideoGameRequestValidator>();
            serviceCollection.AddScoped<IValidator<UpdateVideoGameRequest>, UpdateVideoGameRequestValidator>();
            serviceCollection.AddScoped<IValidator<ReadMostPlayedVideoGameByConsoleIdRequest>, ReadMostPlayedVideoGameByConsoleIdRequestValidator>();
            serviceCollection.AddScoped<IValidator<ReadVideoGameByIdRequest>, ReadVideoGameByIdRequestValidator>();
            serviceCollection.AddScoped<IValidator<ReadVideoGamesByConsoleIdRequest>, ReadVideoGamesByConsoleIdRequestValidator>();

            return serviceCollection;
        }
    }
}
