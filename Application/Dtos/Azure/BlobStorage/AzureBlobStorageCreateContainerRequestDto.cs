namespace Application.Dtos.Azure.BlobStorage
{
    public class AzureBlobStorageCreateContainerRequestDto
    {
        public string BlobContainerName { get; set; } = default!;
    }
}
