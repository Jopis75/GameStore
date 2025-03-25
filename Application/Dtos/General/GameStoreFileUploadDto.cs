using Domain.Dtos;

namespace Application.Dtos.General
{
    public class GameStoreFileUploadDto<T>
        where T : DtoBase, new()
    {
        public T[] Data { get; set; }

        public GameStoreFileUploadDto()
        {
            Data = Array.Empty<T>();
        }

        public GameStoreFileUploadDto(T[] data)
        {
            Data = new T[data.Length];
            data.CopyTo(Data, 0);
        }
    }
}
