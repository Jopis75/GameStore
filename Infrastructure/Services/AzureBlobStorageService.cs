using Application.Dtos.AzureBlobStorage;
using Application.Interfaces.Infrastructure;
using Azure.Storage.Blobs;
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

        public Task<AzureBlobStorageDownloadResponseDto> DownloadAsync(AzureBlobStorageDownloadRequestDto azureBlobStorageDownloadRequestDto)
        {
            throw new NotImplementedException();
        }

        public async Task<AzureBlobStorageUploadResponseDto> UploasAsync(AzureBlobStorageUploadRequestDto azureBlobStorageUploadRequestDto)
        {
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
                    await blobClient.UploadAsync(binaryData, true);
                }
            }

            return new AzureBlobStorageUploadResponseDto();
        }
    }
}
