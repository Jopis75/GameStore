using Application.Dtos.General;
using MediatR;

namespace Application.Features.VideoGames.Requests.Queries
{
    public class DownloadExcelRequest : IRequest<HttpResponseDto<DownloadExcelDto>>
    {
        public int ConsoleId { get; set; }

        public string FileDownloadName { get; set; } = String.Empty;
    }
}
