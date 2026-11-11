using GameStore.Application.Dtos.General;

namespace GameStore.Application.Interfaces.Infrastructure
{
    public interface IExcelFileService
    {
        Task<FileDownloadResponseDto> CreateFileDownloadForVideoGamesByConsoleIdAsync(int consoleId, string fileDownloadName, CancellationToken cancellationToken);

        Task<FileDownloadResponseDto> CreateFileDownloadForVideoGamesByGenreIdAsync(int genreId, string fileDownloadName, CancellationToken cancellationToken);
    }
}
