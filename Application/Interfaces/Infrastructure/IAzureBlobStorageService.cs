using Application.Dtos.AzureBlobStorage;

namespace Application.Interfaces.Infrastructure
{
    public interface IAzureBlobStorageService
    {
        Task CreateContainerAsync(string containerName);

        Task<AzureBlobStorageDownloadResponseDto> DownloadAsync(AzureBlobStorageDownloadRequestDto azureBlobStorageDownloadRequestDto);

        Task<AzureBlobStorageUploadResponseDto> UploasAsync(AzureBlobStorageUploadRequestDto azureBlobStorageUploadRequestDto);
    }
}
