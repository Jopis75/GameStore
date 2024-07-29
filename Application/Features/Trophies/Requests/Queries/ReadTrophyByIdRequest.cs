using Application.Dtos.General;
using Domain.Dtos;
using MediatR;

namespace Application.Features.Trophies.Requests.Queries
{
    public class ReadTrophyByIdRequest : IRequest<HttpResponseDto<TrophyDto>>
    {
        public int Id { get; set; }
    }
}
