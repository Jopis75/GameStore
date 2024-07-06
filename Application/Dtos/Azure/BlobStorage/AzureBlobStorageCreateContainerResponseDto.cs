using Azure.Storage.Blobs;

namespace Application.Dtos.Azure.BlobStorage
{
    public class AzureBlobStorageCreateContainerResponseDto
    {
        public BlobContainerClient? BlobContainerClient { get; set; } = null;
    }
}
