using GameStore.Application.Dtos.General;
using GameStore.Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace GameStore.Application.Features.VideoGames.Requests.Commands
{
    public class UploadGameStoreFileRequest : IRequest<HttpResponseDto<GameStoreFileUploadResponseDto<VideoGameDto>>>
    {
        public IFormFile FormFile { get; set; } = default!;
    }
}
