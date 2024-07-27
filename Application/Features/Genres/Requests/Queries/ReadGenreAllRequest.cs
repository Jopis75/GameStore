using Application.Dtos.General;
using Domain.Dtos;
using MediatR;

namespace Application.Features.Genres.Requests.Queries
{
    public class ReadGenreAllRequest : IRequest<HttpResponseDto<GenreDto>>
    {
    }
}
