using Application.Dtos.General;
using Domain.Dtos;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.Infrastructure
{
    public interface IGameStoreFileService
    {
        Task<UploadGameStoreFileDto<VideoGameDto>> UpsertAsync(IFormFile formFile, CancellationToken cancellationToken);

        Task<UploadGameStoreFileDto<VideoGameDto>> UpsertAsync(Stream stream, CancellationToken cancellationToken);
    }
}
