using Application.Dtos.General;
using Domain.Dtos;
using MediatR;

namespace Application.Features.Genres.Requests.Commands
{
    public class DeleteGenreRequest : IRequest<HttpResponseDto<GenreDto>>
    {
        public int Id { get; set; }
    }
}
