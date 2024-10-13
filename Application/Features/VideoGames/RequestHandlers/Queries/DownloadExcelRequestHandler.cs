using Application.Dtos.General;
using Application.Dtos.General.Interfaces;
using Application.Features.VideoGames.Requests.Queries;
using Application.Interfaces.Persistance;
using ClosedXML.Excel;
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

        private readonly IValidator<DownloadExcelRequest> _validator;

        private readonly ILogger<DownloadExcelRequestHandler> _logger;

        public DownloadExcelRequestHandler(IVideoGameRepository videoGameRepository, IValidator<DownloadExcelRequest> validator, ILogger<DownloadExcelRequestHandler> logger)
        {
            _videoGameRepository = videoGameRepository ?? throw new ArgumentNullException(nameof(videoGameRepository));
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

                // Use ClosedXML package here.
                // https://learn.microsoft.com/en-us/answers/questions/835030/how-to-download-as-excel-file-from-selected-rows-i

                var downloadExcelDto = new DownloadExcelDto
                {
                    ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    FileContents = CreateFileContents(),
                    FileDownloadName = downloadExcelRequest.FileDownloadName
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

        class Employee
        {
            public int EmpID { get; set; }

            public string EmpName { get; set; }
        }

        private byte[] CreateFileContents()
        {
            var testdata = new List<Employee>()
            {
                new Employee(){ EmpID=101, EmpName="Johnny"},
                new Employee(){ EmpID=102, EmpName="Tom"},
                new Employee(){ EmpID=103, EmpName="Jack"},
                new Employee(){ EmpID=104, EmpName="Vivian"},
                new Employee(){ EmpID=105, EmpName="Edward"},
            };
            //using System.Data;  
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[2] { new DataColumn("EmpID"),
                                    new DataColumn("EmpName") });

            foreach (var emp in testdata)
            {
                dt.Rows.Add(emp.EmpID, emp.EmpName);
            }
            //using ClosedXML.Excel;  
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }
    }
}
