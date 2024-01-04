using Application.Dtos.Common;
using Application.Dtos.VideoGames;
using MediatR;

namespace Application.Features.VideoGames.Requests.Queries
{
    public class ReadVideoGameByIdRequest : IRequest<HttpResponseDto<ReadVideoGameResponseDto>>
    {
        public ReadVideoGameByIdRequestDto? ReadVideoGameByIdRequestDto { get; set; }
    }
}
