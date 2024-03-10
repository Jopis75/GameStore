using Application.Dtos.Common;
using Azure.Storage.Blobs;

namespace Application.Dtos.AzureBlobStorage
{
    public class AzureBlobStorageCreateContainerResponseDto : ResponseDto
    {
        public BlobContainerClient BlobContainerClient { get; set; } = default!;
    }
}
