using GameStore.Application.Dtos.General;
using GameStore.Domain.Dtos;
using MediatR;

namespace GameStore.Application.Features.VideoGames.Requests.Commands
{
    public class DeleteVideoGameRequest : IRequest<HttpResponseDto<VideoGameDto>>
    {
        public int Id { get; set; }
    }
}
