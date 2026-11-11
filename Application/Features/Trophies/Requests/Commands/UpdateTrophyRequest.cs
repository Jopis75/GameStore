using GameStore.Application.Dtos.General;
using GameStore.Domain.Dtos;
using GameStore.Domain.Enums;
using MediatR;

namespace GameStore.Application.Features.Trophies.Requests.Commands
{
    public class UpdateTrophyRequest : IRequest<HttpResponseDto<TrophyDto>>
    {
        public string Name { get; set; } = String.Empty;

        public string Description { get; set; } = String.Empty;

        public string IconUrl { get; set; } = String.Empty;

        public int Id { get; set; }

        public TrophyValue TrophyValue { get; set; }

        public int VideoGameId { get; set; }
    }
}
