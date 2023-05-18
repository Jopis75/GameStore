using Application.Dtos.Addresses;
using Application.Dtos.Companies;
using Application.Dtos.Products;
using Application.Dtos.Reviews;
using AutoMapper;
using Domain.Entities;

namespace Application.Profiles
{
    public class GameStoreProfile : Profile
    {
        public GameStoreProfile()
        {
            // Addresses
            CreateMap<Address, CreateAddressRequestDto>().ReverseMap();
            CreateMap<Address, ReadAllAddressResponseDto>().ReverseMap();
            CreateMap<Address, ReadByIdAddressResponseDto>().ReverseMap();
            CreateMap<Address, UpdateAddressRequestDto>().ReverseMap();

            // Companies
            CreateMap<Company, CreateCompanyRequestDto>().ReverseMap();
            CreateMap<Company, ReadAllCompanyResponseDto>().ReverseMap();
            CreateMap<Company, ReadByIdCompanyResponseDto>().ReverseMap();
            CreateMap<Company, UpdateCompanyRequestDto>().ReverseMap();

            // Products
            CreateMap<Product, CreateProductRequestDto>().ReverseMap();
            CreateMap<Product, ReadAllProductResponseDto>().ReverseMap();
            CreateMap<Product, ReadByIdProductResponseDto>().ReverseMap();
            CreateMap<Product, UpdateProductRequestDto>().ReverseMap();

            // Reviews
            CreateMap<Review, CreateReviewRequestDto>().ReverseMap();
            CreateMap<Review, ReadAllReviewResponseDto>().ReverseMap();
            CreateMap<Review, ReadByIdReviewResponseDto>().ReverseMap();
            CreateMap<Review, UpdateReviewRequestDto>().ReverseMap();
        }
    }
}
