using Application.Dtos.General;
using Domain.Dtos;
using MediatR;

namespace Application.Features.Trophies.Requests.Queries
{
    public class ReadTrophyAllRequest : IRequest<HttpResponseDto<TrophyDto>>
    {
    }
}
