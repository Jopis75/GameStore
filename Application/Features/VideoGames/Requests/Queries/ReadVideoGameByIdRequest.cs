using Application.Dtos.General;
using Domain.Dtos;
using MediatR;

namespace Application.Features.VideoGames.Requests.Queries
{
    public class ReadVideoGameByIdRequest : IRequest<HttpResponseDto<VideoGameDto>>
    {
        public int? Id { get; set; }
    }
}
