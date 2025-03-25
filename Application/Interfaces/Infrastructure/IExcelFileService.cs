using Application.Dtos.General;

namespace Application.Interfaces.Infrastructure
{
    public interface IExcelFileService
    {
        Task<FileDownloadDto> CreateFileDownloadForVideoGamesByConsoleIdAsync(int consoleId, string fileDownloadName, CancellationToken cancellationToken);

        Task<FileDownloadDto> CreateFileDownloadForVideoGamesByGenreIdAsync(int genreId, string fileDownloadName, CancellationToken cancellationToken);
    }
}
