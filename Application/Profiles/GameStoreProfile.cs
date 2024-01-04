using Application.Dtos.Addresses;
using Application.Dtos.Companies;
using Application.Dtos.Consoles;
using Application.Dtos.ConsoleVideoGames;
using Application.Dtos.Reviews;
using Application.Dtos.VideoGames;
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
            CreateMap<Address, ReadAllAddressRequestDto>().ReverseMap();
            CreateMap<Address, ReadByIdAddressRequestDto>().ReverseMap();
            CreateMap<Address, ReadAddressResponseDto>().ReverseMap();
            CreateMap<Address, UpdateAddressRequestDto>().ReverseMap();
            CreateMap<Address, UpdateAddressResponseDto>().ReverseMap();
            CreateMap<Address, DeleteAddressRequestDto>().ReverseMap();
            CreateMap<Address, DeleteAddressResponseDto>().ReverseMap();

            // Companies.
            CreateMap<Company, CreateCompanyRequestDto>().ReverseMap();
            CreateMap<Company, CreateCompanyResponseDto>().ReverseMap();
            CreateMap<Company, ReadAllCompanyRequestDto>().ReverseMap();
            CreateMap<Company, ReadByIdCompanyRequestDto>().ReverseMap();
            CreateMap<Company, ReadCompanyResponseDto>().ReverseMap();
            CreateMap<Company, UpdateCompanyRequestDto>().ReverseMap();
            CreateMap<Company, UpdateCompanyResponseDto>().ReverseMap();
            CreateMap<Company, DeleteCompanyRequestDto>().ReverseMap();
            CreateMap<Company, DeleteCompanyResponseDto>().ReverseMap();

            // Consoles.
            CreateMap<Console, CreateConsoleRequestDto>().ReverseMap();
            CreateMap<Console, CreateConsoleResponseDto>().ReverseMap();
            CreateMap<Console, ReadAllConsoleRequestDto>().ReverseMap();
            CreateMap<Console, ReadByIdConsoleRequestDto>().ReverseMap();
            CreateMap<Console, ReadConsoleResponseDto>().ReverseMap();
            CreateMap<Console, UpdateConsoleRequestDto>().ReverseMap();
            CreateMap<Console, UpdateConsoleResponseDto>().ReverseMap();
            CreateMap<Console, DeleteConsoleRequestDto>().ReverseMap();
            CreateMap<Console, DeleteConsoleResponseDto>().ReverseMap();

            // ConsoleVideoGames.
            CreateMap<ConsoleVideoGame, CreateConsoleVideoGameRequestDto>().ReverseMap();
            CreateMap<ConsoleVideoGame, CreateConsoleVideoGameResponseDto>().ReverseMap();
            CreateMap<ConsoleVideoGame, ReadAllConsoleVideoGameRequestDto>().ReverseMap();
            CreateMap<ConsoleVideoGame, ReadByIdConsoleVideoGameRequestDto>().ReverseMap();
            CreateMap<ConsoleVideoGame, ReadConsoleVideoGameResponseDto>().ReverseMap();
            CreateMap<ConsoleVideoGame, UpdateConsoleVideoGameRequestDto>().ReverseMap();
            CreateMap<ConsoleVideoGame, UpdateConsoleVideoGameResponseDto>().ReverseMap();
            CreateMap<ConsoleVideoGame, DeleteConsoleVideoGameRequestDto>().ReverseMap();
            CreateMap<ConsoleVideoGame, DeleteConsoleVideoGameResponseDto>().ReverseMap();

            // VideoGames.
            CreateMap<VideoGame, CreateVideoGameRequestDto>().ReverseMap();
            CreateMap<VideoGame, CreateVideoGameResponseDto>().ReverseMap();
            CreateMap<VideoGame, ReadAllVideoGameRequestDto>().ReverseMap();
            CreateMap<VideoGame, ReadByIdVideoGameRequestDto>().ReverseMap();
            CreateMap<VideoGame, ReadVideoGameResponseDto>().ReverseMap();
            CreateMap<VideoGame, UpdateVideoGameRequestDto>().ReverseMap();
            CreateMap<VideoGame, UpdateVideoGameResponseDto>().ReverseMap();
            CreateMap<VideoGame, DeleteVideoGameRequestDto>().ReverseMap();
            CreateMap<VideoGame, DeleteVideoGameResponseDto>().ReverseMap();

            // Reviews.
            CreateMap<Review, CreateReviewRequestDto>().ReverseMap();
            CreateMap<Review, CreateReviewResponseDto>().ReverseMap();
            CreateMap<Review, ReadAllReviewRequestDto>().ReverseMap();
            CreateMap<Review, ReadByIdReviewRequestDto>().ReverseMap();
            CreateMap<Review, ReadReviewByVideoGameIdRequestDto>().ReverseMap();
            CreateMap<Review, ReadReviewResponseDto>().ReverseMap();
            CreateMap<Review, UpdateReviewRequestDto>().ReverseMap();
            CreateMap<Review, UpdateReviewResponseDto>().ReverseMap();
            CreateMap<Review, DeleteReviewRequestDto>().ReverseMap();
            CreateMap<Review, DeleteReviewResponseDto>().ReverseMap();
        }
    }
}
