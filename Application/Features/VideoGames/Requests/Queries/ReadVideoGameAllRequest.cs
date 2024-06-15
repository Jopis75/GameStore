using Application.Dtos.Common;
using Domain.Dtos;
using MediatR;

namespace Application.Features.VideoGames.Requests.Queries
{
    public class ReadVideoGameAllRequest : IRequest<HttpResponseDto<List<VideoGameDto>>>
    {
    }
}
