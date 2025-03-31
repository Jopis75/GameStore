using Application.Dtos.General;
using MediatR;

namespace Application.Features.VideoGames.Requests.Queries
{
    public class DownloadExcelFileByConsoleIdRequest : IRequest<HttpResponseDto<FileDownloadResponseDto>>
    {
        public int ConsoleId { get; set; }

        public string FileDownloadName { get; set; } = String.Empty;
    }
}
