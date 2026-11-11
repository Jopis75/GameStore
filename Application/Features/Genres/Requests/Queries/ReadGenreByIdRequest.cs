using GameStore.Application.Dtos.General;
using GameStore.Domain.Dtos;
using MediatR;

namespace GameStore.Application.Features.Genres.Requests.Queries
{
    public class ReadGenreByIdRequest : IRequest<HttpResponseDto<GenreDto>>
    {
        public int Id { get; set; }
    }
}
