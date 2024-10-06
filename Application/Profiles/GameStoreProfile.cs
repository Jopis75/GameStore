using Application.Dtos.Identity;
using Application.Features.Addresses.Requests.Commands;
using Application.Features.Companies.Requests.Commands;
using Application.Features.Consoles.Requests.Commands;
using Application.Features.ConsoleVideoGames.Requests.Commands;
using Application.Features.Genres.Requests.Commands;
using Application.Features.Reviews.Requests.Commands;
using Application.Features.Trophies.Requests.Commands;
using Application.Features.VideoGames.Requests.Commands;
using Application.Models.Identity;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Console = Domain.Entities.Console;

namespace Application.Profiles
{
    public class GameStoreProfile : Profile
    {
        public GameStoreProfile()
        {
            // Domain.Dtos.
            CreateMap<Address, AddressDto>()
                .ReverseMap();
            CreateMap<Company, CompanyDto>()
                .ReverseMap();
            CreateMap<Product, ProductDto>()
                .ReverseMap();
            CreateMap<Console, ConsoleDto>()
                .ReverseMap();
            CreateMap<ConsoleVideoGame, ConsoleVideoGameDto>()
                .ReverseMap();
            CreateMap<Review, ReviewDto>()
                .ReverseMap();
            CreateMap<Product, ProductDto>()
                .ReverseMap();
            CreateMap<VideoGame, VideoGameDto>()
                .ReverseMap();

            // Application.Dtos.Identity.
            CreateMap<ApplicationUser, UserDto>()
                .ReverseMap();

            // Application.Features.Addresses.Requests.
            CreateMap<CreateAddressRequest, AddressDto>()
                .ReverseMap();
            CreateMap<UpdateAddressRequest, AddressDto>()
                .ReverseMap();

            // Application.Features.Companies.Requests.
            CreateMap<CreateCompanyRequest, CompanyDto>()
                .ReverseMap();
            CreateMap<CreateCompanyWithHeadquarterRequest, CompanyDto>()
                .ReverseMap();
            CreateMap<UpdateCompanyRequest, CompanyDto>()
                .ReverseMap();

            // Application.Features.Consoles.Requests.
            CreateMap<CreateConsoleRequest, ConsoleDto>()
                .ReverseMap();
            CreateMap<UpdateConsoleRequest, ConsoleDto>()
                .ReverseMap();

            // Application.Features.ConsoleVideoGames.Requests.
            CreateMap<CreateConsoleVideoGameRequest, ConsoleVideoGameDto>()
                .ReverseMap();
            CreateMap<UpdateConsoleVideoGameRequest, ConsoleVideoGameDto>()
                .ReverseMap();

            // Application.Features.Genres.Requests.
            CreateMap<CreateGenreRequest, GenreDto>()
                .ReverseMap();
            CreateMap<UpdateGenreRequest, GenreDto>()
                .ReverseMap();

            // Application.Features.Reviews.Requests.
            CreateMap<CreateReviewRequest, ReviewDto>()
                .ReverseMap();
            CreateMap<UpdateReviewRequest, ReviewDto>()
                .ReverseMap();

            // Application.Features.Trophies.Requests.
            CreateMap<CreateTrophyRequest, TrophyDto>()
                .ReverseMap();
            CreateMap<UpdateTrophyRequest, TrophyDto>()
                .ReverseMap();

            // Application.Features.VideoGames.Requests.
            CreateMap<CreateVideoGameRequest, VideoGameDto>()
                .ReverseMap();
            CreateMap<UpdateVideoGameRequest, VideoGameDto>()
                .ReverseMap();
        }
    }
}
