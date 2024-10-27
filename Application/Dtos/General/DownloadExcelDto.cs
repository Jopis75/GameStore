using Domain.Dtos;

namespace Application.Dtos.General
{
    public class DownloadExcelDto : DtoBase
    {
        public string ContentType { get; set; } = string.Empty;

        public byte[] FileContents { get; set; } = default!;

        public string FileDownloadName { get; set; } = string.Empty;
    }
}
