namespace Application.Dtos.Common.Interfaces
{
    public class ReadResponseDto : ResponseDto
    {
        public DateTime? CreatedAt { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? DeletedAt { get; set; }

        public string? DeletedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string? UpdatedBy { get; set; }
    }
}
