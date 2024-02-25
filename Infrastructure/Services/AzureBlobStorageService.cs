using Application.Dtos.AzureBlobStorage;
using Application.Dtos.Common;
using Application.Interfaces.Infrastructure;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Azure;

namespace Infrastructure.Services
{
    public class AzureBlobStorageService : IAzureBlobStorageService
    {
        private readonly IAzureClientFactory<BlobServiceClient> _azureClientFactory;

        private readonly BlobServiceClient _blobServiceClient;

        public AzureBlobStorageService(IAzureClientFactory<BlobServiceClient> azureClientFactory)
        {
            _azureClientFactory = azureClientFactory ?? throw new ArgumentNullException(nameof(azureClientFactory));
            _blobServiceClient = _azureClientFactory.CreateClient("BlobStorage");
        }

        public Task CreateContainerAsync(string containerName)
        {
            throw new NotImplementedException();
        }

        public Task<HttpResponseDto<AzureBlobStorageDownloadResponseDto>> DownloadAsync(AzureBlobStorageDownloadRequestDto azureBlobStorageDownloadRequestDto)
        {
            throw new NotImplementedException();
        }

        public async Task<HttpResponseDto<AzureBlobStorageUploadResponseDto>> UploasAsync(AzureBlobStorageUploadRequestDto azureBlobStorageUploadRequestDto)
        {
            try
            {
                if (azureBlobStorageUploadRequestDto == null)
                {
                    var httpResponseDto1 = new HttpResponseDto<AzureBlobStorageUploadResponseDto>(new ArgumentNullException(nameof(azureBlobStorageUploadRequestDto)).Message, StatusCodes.Status400BadRequest);
                    return httpResponseDto1;
                }

                var blobContainerClient = _blobServiceClient.GetBlobContainerClient(azureBlobStorageUploadRequestDto.BlobContainerName);
                var fileName = Path.GetFileName(azureBlobStorageUploadRequestDto.Path);
                var blobClient = blobContainerClient.GetBlobClient(fileName);

                using (var fileStream = File.OpenRead(azureBlobStorageUploadRequestDto.Path))
                {
                    using (var binaryReader = new BinaryReader(fileStream))
                    {
                        var buffer = new byte[fileStream.Length];
                        binaryReader.Read(buffer, 0, buffer.Length);

                        var binaryData = new BinaryData(buffer);
                        var blobContentInfo = await blobClient.UploadAsync(binaryData, true);
                        var azureBlobStorageUploadResponseDto = new AzureBlobStorageUploadResponseDto
                        {
                            BlobContentInfo = blobContentInfo
                        };
                        var httpResponseDto = new HttpResponseDto<AzureBlobStorageUploadResponseDto>(azureBlobStorageUploadResponseDto, StatusCodes.Status200OK);
                        return httpResponseDto;
                    }
                }
            }
            catch (Exception ex)
            {
                var httpResponseDto1 = new HttpResponseDto<AzureBlobStorageUploadResponseDto>(ex.Message, StatusCodes.Status500InternalServerError);
                return httpResponseDto1;
            }
        }
    }
}
