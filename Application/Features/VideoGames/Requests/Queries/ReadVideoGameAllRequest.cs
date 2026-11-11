using GameStore.Application.Dtos.General;
using GameStore.Domain.Dtos;
using MediatR;

namespace GameStore.Application.Features.VideoGames.Requests.Queries
{
    public class ReadVideoGameAllRequest : IRequest<HttpResponseDto<VideoGameDto>>
    {
    }
}
