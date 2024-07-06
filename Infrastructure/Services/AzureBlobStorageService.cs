using Application.Dtos.Azure.BlobStorage;
using Application.Dtos.General;
using Application.Interfaces.Infrastructure;
using Azure.Storage.Blobs;
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
            _azureClientFactory = azureClientFactory ?? throw new ArgumentNullException(nameof(azureClientFactory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _blobServiceClient = _azureClientFactory.CreateClient("BlobStorage");
        }

        public async Task<HttpResponseDto<AzureBlobStorageCreateContainerResponseDto>> CreateContainerAsync(AzureBlobStorageCreateContainerRequestDto azureBlobStorageCreateContainerRequestDto)
        {
            try
            {
                _logger.LogInformation("Begin CreateContainerAsync {@AzureBlobStorageCreateContainerRequestDto}.", azureBlobStorageCreateContainerRequestDto);

                if (azureBlobStorageCreateContainerRequestDto == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<AzureBlobStorageCreateContainerResponseDto>(new ArgumentNullException(nameof(azureBlobStorageCreateContainerRequestDto)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error CreateContainerAsync {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var blobContainerClient = await _blobServiceClient.CreateBlobContainerAsync(azureBlobStorageCreateContainerRequestDto.BlobContainerName);

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
                _logger.LogError("Error CreateContainerAsync {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }

        public async Task<HttpResponseDto<AzureBlobStorageDeleteContainerResponseDto>> DeleteContainerAsync(AzureBlobStorageDeleteContainerRequestDto azureBlobStorageDeleteContainerRequestDto)
        {
            try
            {
                _logger.LogInformation("Begin DeleteContainerAsync {@AzureBlobStorageDeleteContainerRequestDto}.", azureBlobStorageDeleteContainerRequestDto);

                if (azureBlobStorageDeleteContainerRequestDto == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<AzureBlobStorageDeleteContainerResponseDto>(new ArgumentNullException(nameof(azureBlobStorageDeleteContainerRequestDto)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error DeleteContainerAsync {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var blobContainerClient = _blobServiceClient.GetBlobContainerClient(azureBlobStorageDeleteContainerRequestDto.BlobContainerName);
                await blobContainerClient.DeleteAsync();

                var azureBlobStorageDeleteContainerResponseDto = new AzureBlobStorageDeleteContainerResponseDto();

                var httpResponseDto = new HttpResponseDto<AzureBlobStorageDeleteContainerResponseDto>(azureBlobStorageDeleteContainerResponseDto, StatusCodes.Status200OK);
                _logger.LogInformation("Done DeleteContainerAsync {@HttpResponseDto}.", httpResponseDto);
                return httpResponseDto;
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<AzureBlobStorageDeleteContainerResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error DeleteContainerAsync {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }

        public async Task<HttpResponseDto<AzureBlobStorageDownloadResponseDto>> DownloadAsync(AzureBlobStorageDownloadRequestDto azureBlobStorageDownloadRequestDto)
        {
            try
            {
                _logger.LogInformation("Begin DownloadAsync {@AzureBlobStorageDownloadRequestDto}.", azureBlobStorageDownloadRequestDto);

                if (azureBlobStorageDownloadRequestDto == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<AzureBlobStorageDownloadResponseDto>(new ArgumentNullException(nameof(azureBlobStorageDownloadRequestDto)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error DownloadAsync {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var blobContainerClient = _blobServiceClient.GetBlobContainerClient(azureBlobStorageDownloadRequestDto.BlobContainerName);
                var blobClient = blobContainerClient.GetBlobClient(Path.GetFileName(azureBlobStorageDownloadRequestDto.Path));

                using (var fileStream = File.OpenWrite(azureBlobStorageDownloadRequestDto.Path))
                {
                    await blobClient.DownloadToAsync(fileStream);

                    var azureBlobStorageDownloadResponseDto = new AzureBlobStorageDownloadResponseDto();

                    var httpResponseDto = new HttpResponseDto<AzureBlobStorageDownloadResponseDto>(azureBlobStorageDownloadResponseDto, StatusCodes.Status200OK);
                    _logger.LogInformation("Done DownloadAsync {@HttpResponseDto}.", httpResponseDto);
                    return httpResponseDto;
                }
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<AzureBlobStorageDownloadResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
                _logger.LogError("Error DownloadAsync {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }

        public async Task<HttpResponseDto<AzureBlobStorageGetByFlatResponseDto>> GetByFlatAsync(AzureBlobStorageGetByFlatRequestDto azureBlobStorageGetByFlatRequestDto)
        {
            try
            {
                _logger.LogInformation("Begin GetByFlatAsync {@AzureBlobStorageGetByFlatRequestDto}.", azureBlobStorageGetByFlatRequestDto);

                if (azureBlobStorageGetByFlatRequestDto == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<AzureBlobStorageGetByFlatResponseDto>(new ArgumentNullException(nameof(azureBlobStorageGetByFlatRequestDto)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error GetByFlatAsync {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var blobContainerClient = _blobServiceClient.GetBlobContainerClient(azureBlobStorageGetByFlatRequestDto.BlobContainerName);
                var pages = blobContainerClient
                    .GetBlobsAsync()
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
                _logger.LogError("Error GetByFlatAsync {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }

        public Task<HttpResponseDto<AzureBlobStorageGetByHierarchyResponseDto>> GetByHierarchyAsync(AzureBlobStorageGetByHierarchyRequestDto azureBlobStorageGetByHierarchyRequestDto)
        {
            throw new NotImplementedException();
        }

        public async Task<HttpResponseDto<AzureBlobStorageUploadResponseDto>> UploadAsync(AzureBlobStorageUploadRequestDto azureBlobStorageUploadRequestDto)
        {
            try
            {
                _logger.LogInformation("Begin UploadAsync {@AzureBlobStorageUploadRequestDto}.", azureBlobStorageUploadRequestDto);

                if (azureBlobStorageUploadRequestDto == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<AzureBlobStorageUploadResponseDto>(new ArgumentNullException(nameof(azureBlobStorageUploadRequestDto)).Message, StatusCodes.Status400BadRequest);
                    _logger.LogError("Error UploadAsync {@HttpResponseDto}.", httpResponseDto1);
                    return httpResponseDto1;
                }

                var blobContainerClient = _blobServiceClient.GetBlobContainerClient(azureBlobStorageUploadRequestDto.BlobContainerName);
                var blobClient = blobContainerClient.GetBlobClient(Path.GetFileName(azureBlobStorageUploadRequestDto.Path));

                using (var fileStream = File.OpenRead(azureBlobStorageUploadRequestDto.Path))
                {
                    var blobContentInfo = (await blobClient.UploadAsync(fileStream, true)).Value;

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
                _logger.LogError("Error UploadAsync {@HttpResponseDto}.", httpResponseDto1);
                return httpResponseDto1;
            }
        }
    }
}
