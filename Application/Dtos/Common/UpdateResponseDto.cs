namespace Application.Dtos.Common
{
    public class UpdateResponseDto : ResponseDto
    {
        public DateTime? UpdatedAt { get; set; }

        public string? UpdatedBy { get; set; }
    }
}
