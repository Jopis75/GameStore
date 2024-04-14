namespace Application.Dtos.Common
{
    public class UpdateRequestDto : RequestDto
    {
        public int Id { get; set; }

        public static DateTime UpdatedAt => DateTime.Now;

        public string? UpdatedBy { get; set; }
    }
}
