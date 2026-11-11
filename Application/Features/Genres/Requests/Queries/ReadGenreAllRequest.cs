using GameStore.Application.Dtos.General;
using GameStore.Domain.Dtos;
using MediatR;

namespace GameStore.Application.Features.Genres.Requests.Queries
{
    public class ReadGenreAllRequest : IRequest<HttpResponseDto<GenreDto>>
    {
    }
}
