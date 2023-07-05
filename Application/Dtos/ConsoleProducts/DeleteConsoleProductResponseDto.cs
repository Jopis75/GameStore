using Application.Dtos.Common.Interfaces;

namespace Application.Dtos.ConsoleProducts
{
    public class DeleteConsoleProductResponseDto : IDeleteResponseDto
    {
        public DateTime? DeletedAt { get; set; }

        public string? DeletedBy { get; set; }

        public int Id { get; set; }
    }
}
