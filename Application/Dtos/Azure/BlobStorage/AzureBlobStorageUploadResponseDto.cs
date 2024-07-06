using Azure.Storage.Blobs.Models;

namespace Application.Dtos.Azure.BlobStorage
{
    public class AzureBlobStorageUploadResponseDto
    {
        public BlobContentInfo? BlobContentInfo { get; set; } = null;
    }
}
