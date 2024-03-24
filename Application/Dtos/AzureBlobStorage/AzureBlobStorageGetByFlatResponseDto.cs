using Azure.Storage.Blobs.Models;

namespace Application.Dtos.AzureBlobStorage
{
    public class AzureBlobStorageGetByFlatResponseDto
    {
        public List<BlobItem> BlobItems { get; set; } = new();
    }
}
