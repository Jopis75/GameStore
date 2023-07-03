using Application.Dtos.Addresses;
using Application.Dtos.Addresses.Validators;
using Application.Dtos.Companies;
using Application.Dtos.Companies.Validators;
using Application.Dtos.Products;
using Application.Dtos.Products.Validators;
using Application.Dtos.Reviews;
using Application.Dtos.Reviews.Validators;
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
            serviceCollection.AddScoped<IValidator<ReadByIdAddressRequestDto>, ReadByIdAddressRequestDtoValidator>();
            serviceCollection.AddScoped<IValidator<UpdateAddressRequestDto>, UpdateAddressRequestDtoValidator>();
            serviceCollection.AddScoped<IValidator<DeleteAddressRequestDto>, DeleteAddressRequestDtoValidator>();

            // Companies.
            serviceCollection.AddScoped<IValidator<CreateCompanyRequestDto>, CreateCompanyRequestDtoValidator>();
            serviceCollection.AddScoped<IValidator<ReadByIdCompanyRequestDto>, ReadByIdCompanyRequestDtoValidator>();
            serviceCollection.AddScoped<IValidator<UpdateCompanyRequestDto>, UpdateCompanyRequestDtoValidator>();
            serviceCollection.AddScoped<IValidator<DeleteCompanyRequestDto>, DeleteCompanyRequestDtoValidator>();

            // Products.
            serviceCollection.AddScoped<IValidator<CreateProductRequestDto>, CreateProductRequestDtoValidator>();
            serviceCollection.AddScoped<IValidator<ReadByIdProductRequestDto>, ReadByIdProductRequestDtoValidator>();
            serviceCollection.AddScoped<IValidator<UpdateProductRequestDto>, UpdateProductRequestDtoValidator>();
            serviceCollection.AddScoped<IValidator<DeleteProductRequestDto>, DeleteProductRequestDtoValidator>();

            // Reviews.
            serviceCollection.AddScoped<IValidator<CreateReviewRequestDto>, CreateReviewRequestDtoValidator>();
            serviceCollection.AddScoped<IValidator<ReadByIdReviewRequestDto>, ReadByIdReviewRequestDtoValidator>();
            serviceCollection.AddScoped<IValidator<UpdateReviewRequestDto>, UpdateReviewRequestDtoValidator>();
            serviceCollection.AddScoped<IValidator<DeleteReviewRequestDto>, DeleteReviewRequestDtoValidator>();

            return serviceCollection;
        }
    }
}
