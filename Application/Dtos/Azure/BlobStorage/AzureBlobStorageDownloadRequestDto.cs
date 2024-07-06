namespace Application.Dtos.Azure.BlobStorage
{
    public class AzureBlobStorageDownloadRequestDto
    {
        public string Path { get; set; } = default!;

        public string BlobContainerName { get; set; } = default!;
    }
}
