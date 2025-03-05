using Application.Dtos.General;
using Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.VideoGames.Requests.Commands
{
    public class UploadGameStoreFileRequest : IRequest<HttpResponseDto<UploadGameStoreFileDto<VideoGameDto>>>
    {
        public IFormFile FormFile { get; set; } = default!;
    }
}
