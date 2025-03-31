using Domain.Dtos;

namespace Application.Dtos.General
{
    public class GameStoreFileUploadResponseDto<TDto>
        where TDto : DtoBase, new()
    {
        public TDto[] Data { get; set; }

        public GameStoreFileUploadResponseDto()
        {
            Data = Array.Empty<TDto>();
        }

        public GameStoreFileUploadResponseDto(TDto[] data)
        {
            Data = new TDto[data.Length];
            data.CopyTo(Data, 0);
        }
    }
}
