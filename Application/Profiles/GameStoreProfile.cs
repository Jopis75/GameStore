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
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<Company, CompanyDto>().ReverseMap();
            CreateMap<Console, ConsoleDto>().ReverseMap();
            CreateMap<ConsoleVideoGame, ConsoleVideoGameDto>().ReverseMap();
            CreateMap<Review, ReviewDto>().ReverseMap();
            CreateMap<VideoGame, VideoGameDto>().ReverseMap();

            // Application.Dtos.Identity.
            CreateMap<ApplicationUser, UserDto>().ReverseMap();
        }
    }
}
