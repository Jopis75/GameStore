using Application.Dtos.General;
using Domain.Dtos;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.Infrastructure
{
    public interface IGameStoreFileService
    {
        Task<GameStoreFileUploadDto<VideoGameDto>> UploadAsync(IFormFile formFile, CancellationToken cancellationToken);

        Task<GameStoreFileUploadDto<VideoGameDto>> UploadAsync(Stream stream, CancellationToken cancellationToken);
    }
}
