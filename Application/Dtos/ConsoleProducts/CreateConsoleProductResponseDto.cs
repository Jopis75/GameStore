using Application.Dtos.Common.Interfaces;

namespace Application.Dtos.ConsoleProducts
{
    public class CreateConsoleProductResponseDto : ICreateResponseDto
    {
        public DateTime? CreatedAt { get; set; }

        public string? CreatedBy { get; set; }

        public int Id { get; set; }
    }
}
