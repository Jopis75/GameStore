using GameStore.Application.Dtos.General;
using GameStore.Domain.Dtos;
using Microsoft.AspNetCore.Http;

namespace GameStore.Application.Interfaces.Infrastructure
{
    public interface IGameStoreFileService
    {
        Task<GameStoreFileUploadResponseDto<VideoGameDto>> UploadAsync(IFormFile formFile, CancellationToken cancellationToken);

        Task<GameStoreFileUploadResponseDto<VideoGameDto>> UploadAsync(Stream stream, CancellationToken cancellationToken);
    }
}
