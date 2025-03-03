using Application.Dtos.General;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.VideoGames.Requests.Commands
{
    public class UploadGameStoreFileRequest : IRequest<HttpResponseDto<UploadGameStoreFileDto>>
    {
        public IFormFile FormFile { get; set; } = default!;
    }
}
