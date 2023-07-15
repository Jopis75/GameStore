using Application.Dtos.Common;
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
