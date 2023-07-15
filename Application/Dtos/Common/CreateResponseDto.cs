namespace Application.Dtos.Common
{
    public class CreateResponseDto : ResponseDto
    {
        public DateTime? CreatedAt { get; set; }

        public string? CreatedBy { get; set; }
    }
}
