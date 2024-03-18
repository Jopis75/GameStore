namespace Application.Dtos.AzureBlobStorage
{
    public class AzureBlobStorageGetByFlatRequestDto
    {
        public string BlobContainerName { get; set; } = default!;

        public string? ContinuationToken { get; set; }

        public int? PageSizeHint { get; set; }
    }
}
