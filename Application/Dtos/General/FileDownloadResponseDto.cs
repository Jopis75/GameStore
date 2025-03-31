namespace Application.Dtos.General
{
    public class FileDownloadResponseDto
    {
        public string ContentType { get; set; } = String.Empty;

        public byte[] FileContents { get; set; } = default!;

        public string FileDownloadName { get; set; } = String.Empty;
    }
}
