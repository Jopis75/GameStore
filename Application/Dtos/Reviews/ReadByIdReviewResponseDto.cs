using Application.Dtos.Common.Interfaces;
using Application.Dtos.Products;

namespace Application.Dtos.Reviews
{
    public class ReadByIdReviewResponseDto : ReadByIdResponseDto
    {
        public int? Grade { get; set; }

        public ReadByIdProductResponseDto? Product { get; set; } = new();

        public DateTime? ReviewDate { get; set; }

        public string? ReviewText { get; set; }
    }
}
