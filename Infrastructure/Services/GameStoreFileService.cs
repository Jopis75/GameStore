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
            memoryStream.Position = 0;

            TextReader textReader = new StreamReader(memoryStream);
            var rows = (await textReader.ReadToEndAsync()).Split("\r\n");

            foreach (var row in rows)
            {
                var columns = row.Split('|');

                var videoGameName = columns[0].Trim();
                var consoleName = columns[1].Trim();
                var developerName = columns[2].Trim();
                var genreName = columns[3].Trim();
                var videoGameReleaseDate = DateTime.Parse(columns[4].Trim());
                var videoGamePurchaseDate = DateTime.Parse(columns[5].Trim());
                var videoGamePrice = Decimal.Parse(columns[6].Trim());
            }

        }

        public Task UpsertAsync(Stream stream)
        {
            return Task.FromResult(0);
        }
    }
}
