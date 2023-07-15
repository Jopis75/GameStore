namespace Application.Dtos.Common.Interfaces
{
    public class CreateResponseDto : ResponseDto
    {
        public DateTime? CreatedAt { get; set; }

        public string? CreatedBy { get; set; }
    }
}
