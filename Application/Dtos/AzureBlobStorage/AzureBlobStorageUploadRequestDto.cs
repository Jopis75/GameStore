namespace Application.Dtos.AzureBlobStorage
{
    public class AzureBlobStorageUploadRequestDto
    {
        public string Path { get; set; } = default!;

        public string BlobContainerName { get; set; } = default!;
    }
}
