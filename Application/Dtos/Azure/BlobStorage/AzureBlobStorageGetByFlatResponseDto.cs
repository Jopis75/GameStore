using Azure.Storage.Blobs.Models;

namespace Application.Dtos.Azure.BlobStorage
{
    public class AzureBlobStorageGetByFlatResponseDto
    {
        public List<BlobItem> BlobItems { get; set; } = new();
    }
}
