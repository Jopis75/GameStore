using Application.Dtos.Common.Interfaces;

namespace Application.Dtos.Products
{
    public class ReadByIdProductRequestDto : IReadByIdRequestDto
    {
        public int Id { get; set; }

        public ReadByIdProductRequestDto(int id)
        {
            Id = id;
        }
    }
}
