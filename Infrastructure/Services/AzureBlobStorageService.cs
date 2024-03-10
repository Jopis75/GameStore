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

        public Task CreateContainerAsync(string containerName)
        {
            throw new NotImplementedException();
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

                    var httpResponseDto = new HttpResponseDto<AzureBlobStorageDownloadResponseDto>(new AzureBlobStorageDownloadResponseDto(), StatusCodes.Status200OK);
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
