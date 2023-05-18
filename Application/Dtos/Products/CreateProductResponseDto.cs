using Application.Dtos.Common.Interfaces;

namespace Application.Dtos.Products
{
    public class CreateProductResponseDto : ICreateResponseDto
    {
        public DateTime? CreatedAt { get; set; }

        public string? CreatedBy { get; set; }

        public int Id { get; set; }
    }
}
