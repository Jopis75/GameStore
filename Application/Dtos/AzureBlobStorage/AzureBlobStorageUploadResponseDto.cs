using Application.Dtos.Common;
using Azure.Storage.Blobs.Models;

namespace Application.Dtos.AzureBlobStorage
{
    public class AzureBlobStorageUploadResponseDto : ResponseDto
    {
        public BlobContentInfo BlobContentInfo { get; set; } = default!;
    }
}
