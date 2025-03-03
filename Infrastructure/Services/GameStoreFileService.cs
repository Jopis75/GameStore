using Application.Interfaces.Infrastructure;
using Application.Interfaces.Persistance;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services
{
    public class GameStoreFileService : IGameStoreFileService
    {
        private readonly IVideoGameRepository _videoGameRepository;

        public GameStoreFileService(IVideoGameRepository videoGameRepository)
        {
            _videoGameRepository = videoGameRepository;
        }

        public async Task UpsertAsync(IFormFile formFile, CancellationToken cancellationToken)
        {
            var memoryStream = new MemoryStream();
            await formFile.CopyToAsync(memoryStream, cancellationToken);
        }

        public Task UpsertAsync(Stream stream)
        {
            return Task.FromResult(0);
        }
    }
}
