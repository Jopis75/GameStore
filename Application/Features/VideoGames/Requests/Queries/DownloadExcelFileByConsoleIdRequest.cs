using Application.Dtos.General;
using MediatR;

namespace Application.Features.VideoGames.Requests.Queries
{
    public class DownloadExcelFileByConsoleIdRequest : IRequest<HttpResponseDto<FileDownloadDto>>
    {
        public int ConsoleId { get; set; }

        public string FileDownloadName { get; set; } = String.Empty;
    }
}
