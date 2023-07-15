using Application.Dtos.Common;

namespace Application.Dtos.ConsoleProducts
{
    public class CreateConsoleProductResponseDto : CreateResponseDto
    {
        public DateTime? CreatedAt { get; set; }

        public string? CreatedBy { get; set; }

        public int Id { get; set; }
    }
}
