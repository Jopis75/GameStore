using GameStore.Application.Dtos.General;
using GameStore.Domain.Dtos;
using MediatR;

namespace GameStore.Application.Features.Trophies.Requests.Commands
{
    public class DeleteTrophyRequest : IRequest<HttpResponseDto<TrophyDto>>
    {
        public int Id { get; set; }
    }
}
