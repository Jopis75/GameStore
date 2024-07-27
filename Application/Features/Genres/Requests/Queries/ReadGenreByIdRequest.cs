using Application.Dtos.General;
using Domain.Dtos;
using MediatR;

namespace Application.Features.Genres.Requests.Queries
{
    public class ReadGenreByIdRequest : IRequest<HttpResponseDto<GenreDto>>
    {
        public int Id { get; set; }
    }
}
