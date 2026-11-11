using GameStore.Application.Dtos.General;
using GameStore.Domain.Dtos;
using MediatR;

namespace GameStore.Application.Features.Genres.Requests.Commands
{
    public class UpdateGenreRequest : IRequest<HttpResponseDto<GenreDto>>
    {
        public int Id { get; set; }

        public string Name { get; set; } = String.Empty;

        public string? Description { get; set; }
    }
}
