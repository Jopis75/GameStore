using GameStore.Domain.Dtos;

namespace GameStore.Application.Dtos.General
{
    public class GameStoreFileUploadResponseDto<TDto>
        where TDto : DtoBase, new()
    {
        public TDto[] Data { get; set; }

        public GameStoreFileUploadResponseDto()
        {
            Data = [];
        }

        public GameStoreFileUploadResponseDto(TDto[] data)
        {
            Data = new TDto[data.Length];
            data.CopyTo(Data, 0);
        }
    }
}
