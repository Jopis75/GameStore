using Application.Dtos.Identity;
using Application.Features.Addresses.Requests.Commands;
using Application.Features.Addresses.Requests.Queries;
using Application.Features.Companies.Requests.Commands;
using Application.Features.Companies.Requests.Queries;
using Application.Validators.Dtos;
using Application.Validators.Identity;
using Application.Validators.Requests.Addresses.Commands;
using Application.Validators.Requests.Addresses.Queries;
using Application.Validators.Requests.Companies.Commands;
using Application.Validators.Requests.Companies.Queries;
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
            // Domain.Dtos.
            serviceCollection.AddScoped<IValidator<AddressDto>, AddressDtoValidator>();
            serviceCollection.AddScoped<IValidator<CompanyDto>, CompanyDtoValidator>();
            serviceCollection.AddScoped<IValidator<ConsoleDto>, ConsoleDtoValidator>();
            serviceCollection.AddScoped<IValidator<ConsoleVideoGameDto>, ConsoleVideoGameDtoValidator>();
            serviceCollection.AddScoped<IValidator<ReviewDto>, CreateReviewRequestDtoValidator>();
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

            return serviceCollection;
        }
    }
}
