using Application.Dtos.Addresses;
using Application.Dtos.Companies;
using Application.Dtos.Consoles;
using Application.Dtos.ConsoleVideoGames;
using Application.Dtos.Identity;
using Application.Dtos.Reviews;
using Application.Dtos.VideoGames;
using Application.Models.Identity;
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
            CreateMap<Address, CreateAddressRequestDto>().ReverseMap();
            CreateMap<Address, CreateAddressResponseDto>().ReverseMap();
            CreateMap<Address, CreateCompanyWithAddressRequestDto>().ReverseMap();
            CreateMap<Address, CreateCompanyWithAddressResponseDto>().ReverseMap();
            CreateMap<Address, ReadAddressResponseDto>().ReverseMap();
            CreateMap<Address, UpdateAddressRequestDto>().ReverseMap();
            CreateMap<Address, UpdateAddressResponseDto>().ReverseMap();
            CreateMap<Address, DeleteAddressRequestDto>().ReverseMap();
            CreateMap<Address, DeleteAddressResponseDto>().ReverseMap();

            // Companies.
            CreateMap<Company, CreateCompanyRequestDto>().ReverseMap();
            CreateMap<Company, CreateCompanyResponseDto>().ReverseMap();
            CreateMap<Company, CreateCompanyWithAddressRequestDto>().ReverseMap();
            CreateMap<Company, CreateCompanyWithAddressResponseDto>().ReverseMap();
            CreateMap<Company, ReadCompanyResponseDto>().ReverseMap();
            CreateMap<Company, UpdateCompanyRequestDto>().ReverseMap();
            CreateMap<Company, UpdateCompanyResponseDto>().ReverseMap();
            CreateMap<Company, DeleteCompanyRequestDto>().ReverseMap();
            CreateMap<Company, DeleteCompanyResponseDto>().ReverseMap();

            // Consoles.
            CreateMap<Console, CreateConsoleRequestDto>().ReverseMap();
            CreateMap<Console, CreateConsoleResponseDto>().ReverseMap();
            CreateMap<Console, ReadConsoleResponseDto>().ReverseMap();
            CreateMap<Console, UpdateConsoleRequestDto>().ReverseMap();
            CreateMap<Console, UpdateConsoleResponseDto>().ReverseMap();
            CreateMap<Console, DeleteConsoleRequestDto>().ReverseMap();
            CreateMap<Console, DeleteConsoleResponseDto>().ReverseMap();

            // ConsoleVideoGames.
            CreateMap<ConsoleVideoGame, CreateConsoleVideoGameRequestDto>().ReverseMap();
            CreateMap<ConsoleVideoGame, CreateConsoleVideoGameResponseDto>().ReverseMap();
            CreateMap<ConsoleVideoGame, ReadConsoleVideoGameResponseDto>().ReverseMap();
            CreateMap<ConsoleVideoGame, UpdateConsoleVideoGameRequestDto>().ReverseMap();
            CreateMap<ConsoleVideoGame, UpdateConsoleVideoGameResponseDto>().ReverseMap();
            CreateMap<ConsoleVideoGame, DeleteConsoleVideoGameRequestDto>().ReverseMap();
            CreateMap<ConsoleVideoGame, DeleteConsoleVideoGameResponseDto>().ReverseMap();

            // Identity.
            CreateMap<ApplicationUser, ReadUserResponseDto>().ReverseMap();

            // Reviews.
            CreateMap<Review, CreateReviewRequestDto>().ReverseMap();
            CreateMap<Review, CreateReviewResponseDto>().ReverseMap();
            CreateMap<Review, ReadReviewByVideoGameIdRequestDto>().ReverseMap();
            CreateMap<Review, ReadReviewResponseDto>().ReverseMap();
            CreateMap<Review, UpdateReviewRequestDto>().ReverseMap();
            CreateMap<Review, UpdateReviewResponseDto>().ReverseMap();
            CreateMap<Review, DeleteReviewRequestDto>().ReverseMap();
            CreateMap<Review, DeleteReviewResponseDto>().ReverseMap();

            // VideoGames.
            CreateMap<VideoGame, CreateVideoGameRequestDto>().ReverseMap();
            CreateMap<VideoGame, CreateVideoGameResponseDto>().ReverseMap();
            CreateMap<VideoGame, ReadVideoGameResponseDto>().ReverseMap();
            CreateMap<VideoGame, UpdateVideoGameRequestDto>().ReverseMap();
            CreateMap<VideoGame, UpdateVideoGameResponseDto>().ReverseMap();
            CreateMap<VideoGame, DeleteVideoGameRequestDto>().ReverseMap();
            CreateMap<VideoGame, DeleteVideoGameResponseDto>().ReverseMap();
        }
    }
}
