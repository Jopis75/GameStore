using Application.Dtos.Common;
using Application.Dtos.VideoGames;
using MediatR;

namespace Application.Features.VideoGames.Requests.Commands
{
    public class UpdateVideoGameRequest : IRequest<HttpResponseDto<UpdateVideoGameResponseDto>>
    {
        public UpdateVideoGameRequestDto? UpdateVideoGameRequestDto { get; set; }
    }
}
