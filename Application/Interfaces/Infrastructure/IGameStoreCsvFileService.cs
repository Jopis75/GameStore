using Microsoft.AspNetCore.Http;

namespace GameStore.Application.Interfaces.Infrastructure
{
    public interface IGameStoreCsvFileService
    {
        Task UploadAsync(IFormFile formFile, CancellationToken cancellationToken);

        Task UploadAsync(Stream stream, CancellationToken cancellationToken);
    }
}
