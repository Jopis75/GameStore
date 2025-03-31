using Application.Dtos.General;
using Domain.Dtos;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.Infrastructure
{
    public interface IGameStoreFileService
    {
        Task<GameStoreFileUploadResponseDto<VideoGameDto>> UploadAsync(IFormFile formFile, CancellationToken cancellationToken);

        Task<GameStoreFileUploadResponseDto<VideoGameDto>> UploadAsync(Stream stream, CancellationToken cancellationToken);
    }
}
