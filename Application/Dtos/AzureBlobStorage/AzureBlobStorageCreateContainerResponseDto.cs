using Azure.Storage.Blobs;

namespace Application.Dtos.AzureBlobStorage
{
    public class AzureBlobStorageCreateContainerResponseDto
    {
        public BlobContainerClient? BlobContainerClient { get; set; } = null;
    }
}
