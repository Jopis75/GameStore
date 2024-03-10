namespace Application.Dtos.AzureBlobStorage
{
    public class AzureBlobStorageDownloadRequestDto
    {
        public string Path { get; set; } = default!;

        public string BlobContainerName { get; set; } = default!;
    }
}
