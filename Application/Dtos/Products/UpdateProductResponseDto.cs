using Application.Dtos.Common.Interfaces;

namespace Application.Dtos.Products
{
    public class UpdateProductResponseDto : IUpdateResponseDto
    {
        public int Id { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string? UpdatedBy { get; set; }
    }
}
