using Application.Dtos.AzureBlobStorage;
using Application.Dtos.Common;
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

        private readonly BlobServiceClient _blobServiceClient;

        private readonly ILogger<AzureBlobStorageService> _logger;

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

                BlobContainerClient blobContainerClient = await _blobServiceClient.CreateBlobContainerAsync(azureBlobStorageCreateContainerRequestDto.ContainerName);

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

                var blobContainerClient = _blobServiceClient.GetBlobContainerClient(azureBlobStorageDeleteContainerRequestDto.ContainerName);
                await blobContainerClient.DeleteAsync();

                var azureBlobStorageDeleteContainerResponseDto = new AzureBlobStorageDeleteContainerResponseDto { };

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

                    var azureBlobStorageDownloadResponseDto = new AzureBlobStorageDownloadResponseDto { };

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
                var fileName = Path.GetFileName(azureBlobStorageUploadRequestDto.Path);
                var blobClient = blobContainerClient.GetBlobClient(fileName);

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
