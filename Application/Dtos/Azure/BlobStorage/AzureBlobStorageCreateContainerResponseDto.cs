using Azure.Storage.Blobs;

namespace GameStore.Application.Dtos.Azure.BlobStorage
{
    public class AzureBlobStorageCreateContainerResponseDto
    {
        public BlobContainerClient? BlobContainerClient { get; set; } = null;
    }
}
