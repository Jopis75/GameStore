using Application.Dtos.Addresses;
using Application.Dtos.Companies;
using Application.Dtos.ConsoleProducts;
using Application.Dtos.Consoles;
using Application.Dtos.Products;
using Application.Dtos.Reviews;
using AutoMapper;
using Domain.Entities;
using Console = Domain.Entities.Console;

namespace Application.Profiles
{
    public class GameStoreProfile : Profile
    {
        public GameStoreProfile()
        {
            // Addresses.
            CreateMap<Address, CreateAddressRequestDto>()
                .ReverseMap();
            CreateMap<Address, CreateAddressResponseDto>()
                .ReverseMap();
            CreateMap<Address, ReadAllAddressRequestDto>()
                .ReverseMap();
            CreateMap<Address, ReadAllAddressResponseDto>()
                .ReverseMap();
            CreateMap<Address, ReadByIdAddressRequestDto>()
                .ReverseMap();
            CreateMap<Address, ReadByIdAddressResponseDto>()
                .ReverseMap();
            CreateMap<Address, UpdateAddressRequestDto>()
                .ReverseMap();
            CreateMap<Address, UpdateAddressResponseDto>()
                .ReverseMap();
            CreateMap<Address, DeleteAddressRequestDto>()
                .ReverseMap();
            CreateMap<Address, DeleteAddressResponseDto>()
                .ReverseMap();

            // Companies.
            CreateMap<Company, CreateCompanyRequestDto>()
                .ReverseMap();
            CreateMap<Company, CreateCompanyResponseDto>()
                .ReverseMap();
            CreateMap<Company, ReadAllCompanyRequestDto>()
                .ReverseMap();
            CreateMap<Company, ReadAllCompanyResponseDto>()
                .ReverseMap();
            CreateMap<Company, ReadByIdCompanyRequestDto>()
                .ReverseMap();
            CreateMap<Company, ReadByIdCompanyResponseDto>()
                .ReverseMap();
            CreateMap<Company, UpdateCompanyRequestDto>()
                .ReverseMap();
            CreateMap<Company, UpdateCompanyResponseDto>()
                .ReverseMap();
            CreateMap<Company, DeleteCompanyRequestDto>()
                .ReverseMap();
            CreateMap<Company, DeleteCompanyResponseDto>()
                .ReverseMap();

            // Consoles.
            CreateMap<Console, CreateConsoleRequestDto>()
                .ReverseMap();
            CreateMap<Console, CreateConsoleResponseDto>()
                .ReverseMap();
            CreateMap<Console, ReadAllConsoleRequestDto>()
                .ReverseMap();
            CreateMap<Console, ReadAllConsoleResponseDto>()
                .ReverseMap();
            CreateMap<Console, ReadByIdConsoleRequestDto>()
                .ReverseMap();
            CreateMap<Console, ReadByIdConsoleResponseDto>()
                .ReverseMap();
            CreateMap<Console, UpdateConsoleRequestDto>()
                .ReverseMap();
            CreateMap<Console, UpdateConsoleResponseDto>()
                .ReverseMap();
            CreateMap<Console, DeleteConsoleRequestDto>()
                .ReverseMap();
            CreateMap<Console, DeleteConsoleResponseDto>()
                .ReverseMap();

            // Products.
            CreateMap<Product, CreateProductRequestDto>()
                .ReverseMap();
            CreateMap<Product, CreateProductResponseDto>()
                .ReverseMap();
            CreateMap<Product, ReadAllProductRequestDto>()
                .ReverseMap();
            CreateMap<Product, ReadAllProductResponseDto>()
                .ReverseMap();
            CreateMap<Product, ReadByIdProductRequestDto>()
                .ReverseMap();
            CreateMap<Product, ReadByIdProductResponseDto>()
                .ReverseMap();
            CreateMap<Product, UpdateProductRequestDto>()
                .ReverseMap();
            CreateMap<Product, UpdateProductResponseDto>()
                .ReverseMap();
            CreateMap<Product, DeleteProductRequestDto>()
                .ReverseMap();
            CreateMap<Product, DeleteProductResponseDto>()
                .ReverseMap();

            // ConsoleProducts.
            CreateMap<ConsoleProduct, CreateConsoleProductRequestDto>()
                .ReverseMap();
            CreateMap<ConsoleProduct, CreateConsoleProductResponseDto>()
                .ReverseMap();
            CreateMap<ConsoleProduct, ReadAllConsoleProductRequestDto>()
                .ReverseMap();
            CreateMap<ConsoleProduct, ReadAllConsoleProductResponseDto>()
                .ReverseMap();
            CreateMap<ConsoleProduct, ReadByIdConsoleProductRequestDto>()
                .ReverseMap();
            CreateMap<ConsoleProduct, ReadByIdConsoleProductResponseDto>()
                .ReverseMap();
            CreateMap<ConsoleProduct, UpdateConsoleProductRequestDto>()
                .ReverseMap();
            CreateMap<ConsoleProduct, UpdateConsoleProductResponseDto>()
                .ReverseMap();
            CreateMap<ConsoleProduct, DeleteConsoleProductRequestDto>()
                .ReverseMap();
            CreateMap<ConsoleProduct, DeleteConsoleProductResponseDto>()
                .ReverseMap();

            // Reviews.
            CreateMap<Review, CreateReviewRequestDto>()
                .ReverseMap();
            CreateMap<Review, CreateReviewResponseDto>()
                .ReverseMap();
            CreateMap<Review, ReadAllReviewRequestDto>()
                .ReverseMap();
            CreateMap<Review, ReadAllReviewResponseDto>()
                .ReverseMap();
            CreateMap<Review, ReadByIdReviewRequestDto>()
                .ReverseMap();
            CreateMap<Review, ReadByIdReviewResponseDto>()
                .ReverseMap();
            CreateMap<Review, UpdateReviewRequestDto>()
                .ReverseMap();
            CreateMap<Review, UpdateReviewResponseDto>()
                .ReverseMap();
            CreateMap<Review, DeleteReviewRequestDto>()
                .ReverseMap();
            CreateMap<Review, DeleteReviewResponseDto>()
                .ReverseMap();
        }
    }
}
