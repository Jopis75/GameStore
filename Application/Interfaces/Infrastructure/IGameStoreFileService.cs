using Application.Dtos.General;
using Domain.Dtos;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.Infrastructure
{
    public interface IGameStoreFileService
    {
        Task<UploadGameStoreFileDto<VideoGameDto>> UploadAsync(IFormFile formFile, CancellationToken cancellationToken);

        Task<UploadGameStoreFileDto<VideoGameDto>> UploadAsync(Stream stream, CancellationToken cancellationToken);
    }
}
