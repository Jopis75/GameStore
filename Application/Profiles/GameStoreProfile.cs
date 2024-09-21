using Application.Dtos.Identity;
using Application.Features.Addresses.Requests.Commands;
using Application.Features.Companies.Requests.Commands;
using Application.Features.Consoles.Requests.Commands;
using Application.Features.ConsoleVideoGames.Requests.Commands;
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
                .MaxDepth(1)
                .ReverseMap()
                .MaxDepth(1);
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Reviews, act => act.MapFrom(src => src.Reviews))
                .Include<Console, ConsoleDto>()
                .MaxDepth(3)
                .ReverseMap()
                .MaxDepth(3);
            CreateMap<Console, ConsoleDto>()
                .MaxDepth(1)
                .ReverseMap()
                .MaxDepth(1);
            CreateMap<ConsoleVideoGame, ConsoleVideoGameDto>()
                .MaxDepth(1)
                .ReverseMap()
                .MaxDepth(1);
            CreateMap<Review, ReviewDto>()
                .MaxDepth(5)
                .ReverseMap()
                .MaxDepth(5);
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Reviews, act => act.MapFrom(src => src.Reviews))
                .Include<VideoGame, VideoGameDto>()
                .ForMember(dest => dest.Reviews, act => act.MapFrom(src => src.Reviews))
                .MaxDepth(5)
                .ReverseMap()
                .MaxDepth(5);
            CreateMap<VideoGame, VideoGameDto>()
                .MaxDepth(2)
                .ReverseMap()
                .MaxDepth(2);

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
            CreateMap<CreateGenreRequest, ConsoleVideoGameDto>()
                .ReverseMap();
            CreateMap<UpdateGenresRequest, ConsoleVideoGameDto>()
                .ReverseMap();

            // Application.Features.Genres.Requests.
            CreateMap<CreateGenreRequest, GenreDto>()
                .ReverseMap();
            CreateMap<UpdateGenresRequest, GenreDto>()
                .ReverseMap();
        }
    }
}
