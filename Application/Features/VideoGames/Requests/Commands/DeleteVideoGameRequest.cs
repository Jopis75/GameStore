using Application.Dtos.Common;
using Application.Dtos.VideoGames;
using MediatR;

namespace Application.Features.VideoGames.Requests.Commands
{
    public class DeleteVideoGameRequest : IRequest<HttpResponseDto<DeleteVideoGameResponseDto>>
    {
        public DeleteVideoGameRequestDto? DeleteVideoGameRequestDto { get; set; }
    }
}
