using Application.Dtos.Common;

namespace Application.Dtos.Consoles
{
    public class CreateConsoleResponseDto : CreateResponseDto
    {
        public DateTime? CreatedAt { get; set; }

        public string? CreatedBy { get; set; }

        public int Id { get; set; }
    }
}
