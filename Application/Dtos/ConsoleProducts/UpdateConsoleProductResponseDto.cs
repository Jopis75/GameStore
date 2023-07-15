using Application.Dtos.Common.Interfaces;

namespace Application.Dtos.ConsoleProducts
{
    public class UpdateConsoleProductResponseDto : UpdateResponseDto
    {
        public int Id { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string? UpdatedBy { get; set; }
    }
}
