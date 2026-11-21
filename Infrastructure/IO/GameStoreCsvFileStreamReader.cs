using GameStore.Application.Dtos.General;
using GameStore.Application.Exceptions;
using GameStore.Application.Interfaces.Infrastructure;
using GameStore.Domain.Dtos;
using Microsoft.AspNetCore.Http;

namespace GameStore.Infrastructure.IO
{
    public class GameStoreCsvFileStreamReader(IGameStoreCsvFileParser gameStoreCsvFileParser) : IGameStoreCsvFileStreamReader
    {
        public async Task<IEnumerable<GameStoreCsvFileDto>> ReadAsync(IFormFile formFile, CancellationToken cancellationToken)
        {
            using var memoryStream = new MemoryStream();

            await formFile.CopyToAsync(memoryStream, cancellationToken);
            memoryStream.Position = 0;

            var gameStoreCsvFileDtos = await ReadAsync(memoryStream, cancellationToken);

            return gameStoreCsvFileDtos;
        }

        public async Task<IEnumerable<GameStoreCsvFileDto>> ReadAsync(Stream stream, CancellationToken cancellationToken)
        {
            var videoGameDtos = new List<VideoGameDto>();

            using var streamReader = new StreamReader(stream);

            var characterString = await streamReader.ReadToEndAsync(cancellationToken);
            var rows = characterString.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var gameStoreCsvFileDtos = await gameStoreCsvFileParser.ParseAsync(rows, cancellationToken);

            return gameStoreCsvFileDtos;
        }
    }
}
