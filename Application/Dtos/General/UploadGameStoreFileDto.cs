using Domain.Dtos;

namespace Application.Dtos.General
{
    public class UploadGameStoreFileDto<T> : DtoBase
        where T : class, new()
    {
        public T[] Data { get; set; }

        public UploadGameStoreFileDto()
        {
            Data = Array.Empty<T>();
            CreatedAt = null;
            CreatedBy = String.Empty;
            DeletedAt = null;
            DeletedBy = String.Empty;
            UpdatedAt = null;
            UpdatedBy = String.Empty;
        }

        public UploadGameStoreFileDto(T[] data, DateTime createdAt, string createdBy)
        {
            Data = new T[data.Length];
            data.CopyTo(Data, 0);
            CreatedAt = createdAt;
            CreatedBy = createdBy;
            DeletedAt = null;
            DeletedBy = String.Empty;
            UpdatedAt = null;
            UpdatedBy = String.Empty;
        }
    }
}
