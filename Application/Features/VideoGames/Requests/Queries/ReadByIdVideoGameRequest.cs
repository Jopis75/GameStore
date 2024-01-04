using Application.Dtos.Common;
using Application.Dtos.VideoGames;
using MediatR;

namespace Application.Features.VideoGames.Requests.Queries
{
    public class ReadByIdVideoGameRequest : IRequest<HttpResponseDto<ReadVideoGameResponseDto>>
    {
        public ReadByIdVideoGameRequestDto? ReadByIdVideoGameRequestDto { get; set; }
    }
}
