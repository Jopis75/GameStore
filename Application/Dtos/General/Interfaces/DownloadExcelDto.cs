using Domain.Dtos;

namespace Application.Dtos.General.Interfaces
{
    public class DownloadExcelDto : DtoBase
    {
        public string ContentType { get; set; } = String.Empty;

        public byte[] FileContents { get; set; } = default!;

        public string FileDownloadName { get; set; } = String.Empty;
    }
}
