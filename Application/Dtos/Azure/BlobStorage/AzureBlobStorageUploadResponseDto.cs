using Azure.Storage.Blobs.Models;

namespace GameStore.Application.Dtos.Azure.BlobStorage
{
    public class AzureBlobStorageUploadResponseDto
    {
        public BlobContentInfo? BlobContentInfo { get; set; } = null;
    }
}
