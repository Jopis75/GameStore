using GameStore.Application.Dtos.General;
using GameStore.Domain.Dtos;
using MediatR;

namespace GameStore.Application.Features.Trophies.Requests.Queries
{
    public class ReadTrophyAllRequest : IRequest<HttpResponseDto<TrophyDto>>
    {
    }
}
