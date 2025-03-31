using Application.Dtos.Azure.BlobStorage;
using Application.Dtos.General;
using Application.Interfaces.Infrastructure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services
{
    public class AzureBlobStorageService : IAzureBlobStorageService
    {
        private readonly IAzureClientFactory<BlobServiceClient> _azureClientFactory;

        private readonly ILogger<AzureBlobStorageService> _logger;

        private readonly BlobServiceClient _blobServiceClient;

        public AzureBlobStorageService(IAzureClientFactory<BlobServiceClient> azureClientFactory, ILogger<AzureBlobStorageService> logger)
        {
            _azureClientFactory = azureClientFactory;
            _logger = logger;

            _blobServiceClient = _azureClientFactory.CreateClient("BlobStorage");
        }

        public async Task<HttpResponseDto<AzureBlobStorageCreateContainerResponseDto>> CreateContainerAsync(AzureBlobStorageCreateContainerRequestDto azureBlobStorageCreateContainerRequestDto, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin CreateContainerAsync {@AzureBlobStorageCreateContainerRequestDto}.", azureBlobStorageCreateContainerRequestDto);

                if (azureBlobStorageCreateContainerRequestDto == null)
                {
                    var ex = new ArgumentNullException(nameof(azureBlobStorageCreateContainerRequestDto));
                    var httpResponseDto1 = new HttpResponseDto<AzureBlobStorageCreateContainerResponseDto>(ex.Message, StatusCodes.Status400BadRequest);
                    _logger.LogError(ex, "Error CreateContainerAsync {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var blobContainerClient = await _blobServiceClient.CreateBlobContainerAsync(azureBlobStorageCreateContainerRequestDto.BlobContainerName, PublicAccessType.None, null, cancellationToken);

                var azureBlobStorageCreateContainerResponseDto = new AzureBlobStorageCreateContainerResponseDto
                {
                    BlobContainerClient = blobContainerClient
                };

                var httpResponseDto = new HttpResponseDto<AzureBlobStorageCreateContainerResponseDto>(azureBlobStorageCreateContainerResponseDto, StatusCodes.Status200OK);
                _logger.LogInformation("Done CreateContainerAsync {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<AzureBlobStorageCreateContainerResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError(ex, "Error CreateContainerAsync {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }

        public async Task<HttpResponseDto<AzureBlobStorageDeleteContainerResponseDto>> DeleteContainerAsync(AzureBlobStorageDeleteContainerRequestDto azureBlobStorageDeleteContainerRequestDto, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin DeleteContainerAsync {@AzureBlobStorageDeleteContainerRequestDto}.", azureBlobStorageDeleteContainerRequestDto);

                if (azureBlobStorageDeleteContainerRequestDto == null)
                {
                    var ex = new ArgumentNullException(nameof(azureBlobStorageDeleteContainerRequestDto));
                    var httpResponseDto1 = new HttpResponseDto<AzureBlobStorageDeleteContainerResponseDto>(ex.Message, StatusCodes.Status400BadRequest);
                    _logger.LogError(ex, "Error DeleteContainerAsync {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var blobContainerClient = _blobServiceClient.GetBlobContainerClient(azureBlobStorageDeleteContainerRequestDto.BlobContainerName);

                var response = await blobContainerClient.DeleteAsync(null, cancellationToken);

                var azureBlobStorageDeleteContainerResponseDto = new AzureBlobStorageDeleteContainerResponseDto(); // ToDo: Populate the response object

                var httpResponseDto = new HttpResponseDto<AzureBlobStorageDeleteContainerResponseDto>(azureBlobStorageDeleteContainerResponseDto, StatusCodes.Status200OK);
                _logger.LogInformation("Done DeleteContainerAsync {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<AzureBlobStorageDeleteContainerResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError(ex, "Error DeleteContainerAsync {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }

        public async Task<HttpResponseDto<AzureBlobStorageDownloadResponseDto>> DownloadAsync(AzureBlobStorageDownloadRequestDto azureBlobStorageDownloadRequestDto, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin DownloadAsync {@AzureBlobStorageDownloadRequestDto}.", azureBlobStorageDownloadRequestDto);

                if (azureBlobStorageDownloadRequestDto == null)
                {
                    var ex = new ArgumentNullException(nameof(azureBlobStorageDownloadRequestDto));
                    var httpResponseDto1 = new HttpResponseDto<AzureBlobStorageDownloadResponseDto>(ex.Message, StatusCodes.Status400BadRequest);
                    _logger.LogError(ex, "Error DownloadAsync {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var blobContainerClient = _blobServiceClient.GetBlobContainerClient(azureBlobStorageDownloadRequestDto.BlobContainerName);

                var blobClient = blobContainerClient.GetBlobClient(Path.GetFileName(azureBlobStorageDownloadRequestDto.Path));

                using (var fileStream = File.OpenWrite(azureBlobStorageDownloadRequestDto.Path))
                {
                    var response = await blobClient.DownloadToAsync(fileStream, cancellationToken); // ToDo: Populate the response object

                    var azureBlobStorageDownloadResponseDto = new AzureBlobStorageDownloadResponseDto();

                    var httpResponseDto = new HttpResponseDto<AzureBlobStorageDownloadResponseDto>(azureBlobStorageDownloadResponseDto, StatusCodes.Status200OK);
                    _logger.LogInformation("Done DownloadAsync {@HttpResponseDto}.", httpResponseDto);
                    return httpResponseDto;
                }
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<AzureBlobStorageDownloadResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError(ex, "Error DownloadAsync {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }

        public async Task<HttpResponseDto<AzureBlobStorageGetByFlatResponseDto>> GetByFlatAsync(AzureBlobStorageGetByFlatRequestDto azureBlobStorageGetByFlatRequestDto, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin GetByFlatAsync {@AzureBlobStorageGetByFlatRequestDto}.", azureBlobStorageGetByFlatRequestDto);

                if (azureBlobStorageGetByFlatRequestDto == null)
                {
                    var ex = new ArgumentNullException(nameof(azureBlobStorageGetByFlatRequestDto));
                    var httpResponseDto1 = new HttpResponseDto<AzureBlobStorageGetByFlatResponseDto>(ex.Message, StatusCodes.Status400BadRequest);
                    _logger.LogError(ex, "Error GetByFlatAsync {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var blobContainerClient = _blobServiceClient.GetBlobContainerClient(azureBlobStorageGetByFlatRequestDto.BlobContainerName);

                var pages = blobContainerClient
                    .GetBlobsAsync(BlobTraits.None, BlobStates.None, null, cancellationToken)
                    .AsPages(azureBlobStorageGetByFlatRequestDto.ContinuationToken, azureBlobStorageGetByFlatRequestDto.PageSizeHint);

                var azureBlobStorageGetByFlatResponseDto = new AzureBlobStorageGetByFlatResponseDto();

                // Enumerate the blobs returned for each page.
                await foreach (var page in pages)
                {
                    azureBlobStorageGetByFlatResponseDto.BlobItems.AddRange(page.Values);
                }

                var httpResponseDto = new HttpResponseDto<AzureBlobStorageGetByFlatResponseDto>(azureBlobStorageGetByFlatResponseDto, StatusCodes.Status200OK);
                _logger.LogInformation("Done GetByFlatAsync {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<AzureBlobStorageGetByFlatResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError(ex, "Error GetByFlatAsync {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }

        public Task<HttpResponseDto<AzureBlobStorageGetByHierarchyResponseDto>> GetByHierarchyAsync(AzureBlobStorageGetByHierarchyRequestDto azureBlobStorageGetByHierarchyRequestDto, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<HttpResponseDto<AzureBlobStorageUploadResponseDto>> UploadAsync(AzureBlobStorageUploadRequestDto azureBlobStorageUploadRequestDto, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Begin UploadAsync {@AzureBlobStorageUploadRequestDto}.", azureBlobStorageUploadRequestDto);

                if (azureBlobStorageUploadRequestDto == null)
                {
                    var ex = new ArgumentNullException(nameof(azureBlobStorageUploadRequestDto));
                    var httpResponseDto1 = new HttpResponseDto<AzureBlobStorageUploadResponseDto>(ex.Message, StatusCodes.Status400BadRequest);
                    _logger.LogError(ex, "Error UploadAsync {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var blobContainerClient = _blobServiceClient.GetBlobContainerClient(azureBlobStorageUploadRequestDto.BlobContainerName);

                var blobClient = blobContainerClient.GetBlobClient(Path.GetFileName(azureBlobStorageUploadRequestDto.Path));

                using (var fileStream = File.OpenRead(azureBlobStorageUploadRequestDto.Path))
                {
                    var blobContentInfo = (await blobClient.UploadAsync(fileStream, true, cancellationToken)).Value;

                    var azureBlobStorageUploadResponseDto = new AzureBlobStorageUploadResponseDto
                    {
                        BlobContentInfo = blobContentInfo
                    };

                    var httpResponseDto = new HttpResponseDto<AzureBlobStorageUploadResponseDto>(azureBlobStorageUploadResponseDto, StatusCodes.Status200OK);
                    _logger.LogInformation("Done UploadAsync {@HttpResponseDto}.", httpResponseDto);
                    return httpResponseDto;
                }
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<AzureBlobStorageUploadResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError(ex, "Error UploadAsync {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
