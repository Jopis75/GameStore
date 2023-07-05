using Application.Dtos.Common.Interfaces;

namespace Application.Dtos.Consoles
{
    public class DeleteConsoleResponseDto : IDeleteResponseDto
    {
        public DateTime? DeletedAt { get; set; }

        public string? DeletedBy { get; set; }

        public int Id { get; set; }
    }
}
