namespace Application.Dtos.Common.Interfaces
{
    public class DeleteResponseDto : ResponseDto
    {
        public DateTime? DeletedAt { get; set; }

        public string? DeletedBy { get; set; }
    }
}
