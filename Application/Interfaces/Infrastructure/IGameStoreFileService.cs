using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.Infrastructure
{
    public interface IGameStoreFileService
    {
        Task UpsertAsync(IFormFile formFile, CancellationToken cancellationToken);

        Task UpsertAsync(Stream stream);
    }
}
