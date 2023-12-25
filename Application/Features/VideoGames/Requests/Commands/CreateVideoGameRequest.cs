using Application.Dtos.Common;
using Application.Dtos.VideoGames;
using MediatR;

namespace Application.Features.VideoGames.Requests.Commands
{
    public class CreateVideoGameRequest : IRequest<HttpResponseDto<CreateVideoGameResponseDto>>
    {
        public CreateVideoGameRequestDto? CreateVideoGameRequestDto { get; set; }
    }
}
