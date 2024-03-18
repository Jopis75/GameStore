using Application.Dtos.AzureBlobStorage;
using Application.Dtos.Common;

namespace Application.Interfaces.Infrastructure
{
    public interface IAzureBlobStorageService
    {
        Task<HttpResponseDto<AzureBlobStorageCreateContainerResponseDto>> CreateContainerAsync(AzureBlobStorageCreateContainerRequestDto azureBlobStorageCreateContainerRequestDto);

        Task<HttpResponseDto<AzureBlobStorageDeleteContainerResponseDto>> DeleteContainerAsync(AzureBlobStorageDeleteContainerRequestDto azureBlobStorageDeleteContainerRequestDto);

        Task<HttpResponseDto<AzureBlobStorageDownloadResponseDto>> DownloadAsync(AzureBlobStorageDownloadRequestDto azureBlobStorageDownloadRequestDto);

        Task<HttpResponseDto<AzureBlobStorageGetByFlatResponseDto>> GetByFlatAsync(AzureBlobStorageGetByFlatRequestDto azureBlobStorageGetByFlatRequestDto);

        Task<HttpResponseDto<AzureBlobStorageGetByHierarchyResponseDto>> GetByHierarchyAsync(AzureBlobStorageGetByHierarchyRequestDto azureBlobStorageGetByHierarchyRequestDto);

        Task<HttpResponseDto<AzureBlobStorageUploadResponseDto>> UploadAsync(AzureBlobStorageUploadRequestDto azureBlobStorageUploadRequestDto);
    }
}
