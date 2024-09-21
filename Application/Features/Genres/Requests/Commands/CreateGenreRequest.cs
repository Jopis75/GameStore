using Application.Dtos.General;
using Domain.Dtos;
using MediatR;

namespace Application.Features.Genres.Requests.Commands
{
    public class CreateGenreRequest : IRequest<HttpResponseDto<GenreDto>>
    {
        public string Name { get; set; } = String.Empty;

        public string? Description { get; set; }
    }
}
