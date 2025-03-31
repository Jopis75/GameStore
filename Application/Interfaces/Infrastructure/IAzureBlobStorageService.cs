using Application.Dtos.Azure.BlobStorage;
using Application.Dtos.General;

namespace Application.Interfaces.Infrastructure
{
    public interface IAzureBlobStorageService
    {
        Task<HttpResponseDto<AzureBlobStorageCreateContainerResponseDto>> CreateContainerAsync(AzureBlobStorageCreateContainerRequestDto azureBlobStorageCreateContainerRequestDto, CancellationToken cancellationToken);

        Task<HttpResponseDto<AzureBlobStorageDeleteContainerResponseDto>> DeleteContainerAsync(AzureBlobStorageDeleteContainerRequestDto azureBlobStorageDeleteContainerRequestDto, CancellationToken cancellationToken);

        Task<HttpResponseDto<AzureBlobStorageDownloadResponseDto>> DownloadAsync(AzureBlobStorageDownloadRequestDto azureBlobStorageDownloadRequestDto, CancellationToken cancellationToken);

        Task<HttpResponseDto<AzureBlobStorageGetByFlatResponseDto>> GetByFlatAsync(AzureBlobStorageGetByFlatRequestDto azureBlobStorageGetByFlatRequestDto, CancellationToken cancellationToken);

        Task<HttpResponseDto<AzureBlobStorageGetByHierarchyResponseDto>> GetByHierarchyAsync(AzureBlobStorageGetByHierarchyRequestDto azureBlobStorageGetByHierarchyRequestDto, CancellationToken cancellationToken);

        Task<HttpResponseDto<AzureBlobStorageUploadResponseDto>> UploadAsync(AzureBlobStorageUploadRequestDto azureBlobStorageUploadRequestDto, CancellationToken cancellationToken);
    }
}
