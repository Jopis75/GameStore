using Application.Dtos.Common;
using Application.Dtos.ConsoleVideoGames;
using MediatR;

namespace Application.Features.ConsoleVideoGames.Requests.Commands
{
    public class CreateConsoleVideoGameRequest : IRequest<HttpResponseDto<CreateConsoleVideoGameResponseDto>>
    {
        public CreateConsoleVideoGameRequestDto? CreateConsoleVideoGameRequestDto { get; set; }
    }
}
