using Application.Dtos.AzureBlobStorage;
using Application.Dtos.Common;

namespace Application.Interfaces.Infrastructure
{
    public interface IAzureBlobStorageService
    {
        Task CreateContainerAsync(string containerName);

        Task<HttpResponseDto<AzureBlobStorageDownloadResponseDto>> DownloadAsync(AzureBlobStorageDownloadRequestDto azureBlobStorageDownloadRequestDto);

        Task<HttpResponseDto<AzureBlobStorageUploadResponseDto>> UploadAsync(AzureBlobStorageUploadRequestDto azureBlobStorageUploadRequestDto);
    }
}
