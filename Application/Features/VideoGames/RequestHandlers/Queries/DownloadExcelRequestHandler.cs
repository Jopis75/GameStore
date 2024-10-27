using Application.Dtos.General;
using Application.Features.VideoGames.Requests.Queries;
using Application.Interfaces.Persistance;
using ClosedXML.Excel;
using Domain.Dtos;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Data;

namespace Application.Features.VideoGames.RequestHandlers.Queries
{
    public class DownloadExcelRequestHandler : IRequestHandler<DownloadExcelRequest, HttpResponseDto<DownloadExcelDto>>
    {
        private readonly IVideoGameRepository _videoGameRepository;

        private readonly IConsoleRepository _consoleRepository;

        private readonly IValidator<DownloadExcelRequest> _validator;

        private readonly ILogger<DownloadExcelRequestHandler> _logger;

        public DownloadExcelRequestHandler(IVideoGameRepository videoGameRepository, IConsoleRepository consoleRepository, IValidator<DownloadExcelRequest> validator, ILogger<DownloadExcelRequestHandler> logger)
        {
            _videoGameRepository = videoGameRepository ?? throw new ArgumentNullException(nameof(videoGameRepository));
            _consoleRepository = consoleRepository ?? throw new ArgumentNullException(nameof(consoleRepository));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<HttpResponseDto<DownloadExcelDto>> Handle(DownloadExcelRequest downloadExcelRequest, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin DownloadExcel {@DownloadExcelRequest}.", downloadExcelRequest);

                if (downloadExcelRequest == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<DownloadExcelDto>(new ArgumentNullException(nameof(downloadExcelRequest)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error DownloadExcel {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var validationResult = await _validator.ValidateAsync(downloadExcelRequest, cancellationToken);

                if (validationResult.IsValid == false)
                {
                    var httpResponseDto1 = new HttpResponseDto<DownloadExcelDto>(new ValidationException(validationResult.Errors).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error DownloadExcel {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var videoGameDtos = await _videoGameRepository.ReadByConsoleIdAsync(downloadExcelRequest.ConsoleId, cancellationToken);
                var console = await _consoleRepository.ReadByIdAsync(downloadExcelRequest.ConsoleId, cancellationToken);

                var fileContents = GetFileContents(videoGameDtos, console.Name);

                var downloadExcelDto = new DownloadExcelDto
                {
                    ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    FileContents = fileContents,
                    FileDownloadName = downloadExcelRequest.FileDownloadName,
                    CreatedAt = DateTime.Now,
                    CreatedBy = String.Empty
                };

                var httpResponseDto = new HttpResponseDto<DownloadExcelDto>(downloadExcelDto, StatusCodes.Status200OK);
                _logger.LogInformation("Done DownloadExcel {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (OperationCanceledException ex)
            {
                var httpResponseDto1 = new HttpResponseDto<DownloadExcelDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Canceled DownloadExcel {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<DownloadExcelDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error DownloadExcel {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }

        private byte[] GetFileContents(IEnumerable<VideoGameDto> videoGameDtos, string consoleName)
        {
            var dataTable = new DataTable($"{consoleName} Games");
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

            using (var workbook = new XLWorkbook())
            {
                workbook.Worksheets.Add(dataTable);

                using (var memoryStream = new MemoryStream())
                {
                    workbook.SaveAs(memoryStream);
                    return memoryStream.ToArray();
                }
            }
        }
    }
}
