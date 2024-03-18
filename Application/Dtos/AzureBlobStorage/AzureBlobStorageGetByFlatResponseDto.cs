using Application.Dtos.Common;
using Azure.Storage.Blobs.Models;

namespace Application.Dtos.AzureBlobStorage
{
    public class AzureBlobStorageGetByFlatResponseDto : ResponseDto
    {
        public List<BlobItem> BlobItems { get; set; } = new();
    }
}
