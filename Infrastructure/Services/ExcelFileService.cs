using Application.Dtos.General;
using Application.Interfaces.Infrastructure;
using Application.Interfaces.Persistance;
using ClosedXML.Excel;
using System.Data;

namespace Infrastructure.Services
{
    public class ExcelFileService(IUnitOfWork unitOfWork) : IExcelFileService
    {
        public async Task<FileDownloadResponseDto> CreateFileDownloadForVideoGamesByConsoleIdAsync(int consoleId, string fileDownloadName, CancellationToken cancellationToken)
        {
            var consoleDto = await unitOfWork.ConsoleRepository.ReadByIdAsync(consoleId, cancellationToken);

            var videoGameDtos = await unitOfWork.VideoGameRepository.ReadByConsoleIdAsync(consoleId, cancellationToken);

            var dataTable = new DataTable($"{consoleDto.Name} Games");
            dataTable.Columns.Add("Title", typeof(string));
            dataTable.Columns.Add("Developer", typeof(string));
            dataTable.Columns.Add("Platform", typeof(string));
            dataTable.Columns.Add("Genre", typeof(string));
            dataTable.Columns.Add("Release date", typeof(DateTime));
            dataTable.Columns.Add("Purchase date", typeof(DateTime));
            dataTable.Columns.Add("Price (SEK)", typeof(decimal));
            dataTable.Columns.Add("Total time played", typeof(TimeSpan));

            foreach (var videoGameDto in videoGameDtos)
            {
                dataTable.Rows.Add(
                    videoGameDto.Title,
                    $"{videoGameDto.Developer.Name} ({videoGameDto.Developer.TradeName})",
                    videoGameDto.ConsoleVideoGames.First().Console.Name,
                    String.Join(", ", videoGameDto.VideoGameGenres.Select(genre => genre.Genre.Name)),
                    videoGameDto.ReleaseDate.Date,
                    videoGameDto.PurchaseDate.Date,
                    videoGameDto.Price,
                    videoGameDto.TotalTimePlayed
                );
            }

            byte[] fileContents;

            using (var workbook = new XLWorkbook())
            {
                workbook.Worksheets.Add(dataTable);

                using (var memoryStream = new MemoryStream())
                {
                    workbook.SaveAs(memoryStream);
                    fileContents = memoryStream.ToArray();
                }
            }

            var fileDownloadResponseDto = new FileDownloadResponseDto
            {
                ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                FileContents = fileContents,
                FileDownloadName = fileDownloadName
            };

            return fileDownloadResponseDto;
        }

        public Task<FileDownloadResponseDto> CreateFileDownloadForVideoGamesByGenreIdAsync(int genreId, string fileDownloadName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
