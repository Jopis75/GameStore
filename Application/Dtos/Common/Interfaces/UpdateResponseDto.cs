namespace Application.Dtos.Common.Interfaces
{
    public class UpdateResponseDto : ResponseDto
    {
        public DateTime? UpdatedAt { get; set; }

        public string? UpdatedBy { get; set; }
    }
}
