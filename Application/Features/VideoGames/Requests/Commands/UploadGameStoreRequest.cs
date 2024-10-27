using Application.Dtos.General;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.VideoGames.Requests.Commands
{
    public class UploadGameStoreRequest : IRequest<HttpResponseDto<UploadGameStoreDto>>
    {
        public IFormFile FormFile { get; set; } = default!;
    }
}
