using Application.Dtos.Identity;
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
        }
    }
}
