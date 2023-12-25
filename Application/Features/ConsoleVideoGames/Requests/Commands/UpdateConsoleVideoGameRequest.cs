using Application.Dtos.Common;
using Application.Dtos.ConsoleVideoGames;
using MediatR;

namespace Application.Features.ConsoleVideoGames.Requests.Commands
{
    public class UpdateConsoleVideoGameRequest : IRequest<HttpResponseDto<UpdateConsoleVideoGameResponseDto>>
    {
        public UpdateConsoleVideoGameRequestDto? UpdateConsoleVideoGameRequestDto { get; set; }
    }
}
