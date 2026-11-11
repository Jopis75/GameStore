using GameStore.Application.Dtos.General;
using GameStore.Domain.Dtos;
using MediatR;

namespace GameStore.Application.Features.Genres.Requests.Commands
{
    public class DeleteGenreRequest : IRequest<HttpResponseDto<GenreDto>>
    {
        public int Id { get; set; }
    }
}
