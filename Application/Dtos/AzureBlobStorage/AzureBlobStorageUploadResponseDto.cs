using Azure.Storage.Blobs.Models;

namespace Application.Dtos.AzureBlobStorage
{
    public class AzureBlobStorageUploadResponseDto
    {
        public BlobContentInfo? BlobContentInfo { get; set; } = null;
    }
}
