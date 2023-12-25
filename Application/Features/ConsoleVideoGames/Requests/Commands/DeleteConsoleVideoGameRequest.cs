using Application.Dtos.Common;
using Application.Dtos.ConsoleVideoGames;
using MediatR;

namespace Application.Features.ConsoleVideoGames.Requests.Commands
{
    public class DeleteConsoleVideoGameRequest : IRequest<HttpResponseDto<DeleteConsoleVideoGameResponseDto>>
    {
        public DeleteConsoleVideoGameRequestDto? DeleteConsoleVideoGameRequestDto { get; set; }
    }
}
