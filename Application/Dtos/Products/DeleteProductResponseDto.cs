using Application.Dtos.Common.Interfaces;

namespace Application.Dtos.Products
{
    public class DeleteProductResponseDto : IDeleteResponseDto
    {
        public DateTime? DeletedAt { get; set; }

        public string? DeletedBy { get; set; }

        public int Id { get; set; }
    }
}
