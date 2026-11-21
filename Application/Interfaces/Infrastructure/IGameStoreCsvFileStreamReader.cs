using GameStore.Application.Dtos.General;
using Microsoft.AspNetCore.Http;

namespace GameStore.Application.Interfaces.Infrastructure
{
    public interface IGameStoreCsvFileStreamReader
    {
        Task<IEnumerable<GameStoreCsvFileDto>> ReadAsync(IFormFile formFile, CancellationToken cancellationToken);

        Task<IEnumerable<GameStoreCsvFileDto>> ReadAsync(Stream stream, CancellationToken cancellationToken);
    }
}
