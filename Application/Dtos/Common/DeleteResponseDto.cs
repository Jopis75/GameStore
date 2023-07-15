namespace Application.Dtos.Common
{
    public class DeleteResponseDto : ResponseDto
    {
        public DateTime? DeletedAt { get; set; }

        public string? DeletedBy { get; set; }
    }
}
