using GameStore.Application.Dtos.General;
using GameStore.Domain.Dtos;
using MediatR;

namespace GameStore.Application.Features.Genres.Requests.Commands
{
    public class CreateGenreRequest : IRequest<HttpResponseDto<GenreDto>>
    {
        public string Name { get; set; } = String.Empty;

        public string? Description { get; set; }
    }
}
