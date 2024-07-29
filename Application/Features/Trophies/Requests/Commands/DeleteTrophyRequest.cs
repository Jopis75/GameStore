using Application.Dtos.General;
using Domain.Dtos;
using MediatR;

namespace Application.Features.Trophies.Requests.Commands
{
    public class DeleteTrophyRequest : IRequest<HttpResponseDto<TrophyDto>>
    {
        public int Id { get; set; }
    }
}
