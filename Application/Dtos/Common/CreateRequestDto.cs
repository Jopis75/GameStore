namespace Application.Dtos.Common
{
    public class CreateRequestDto : RequestDto
    {
        public static DateTime CreatedAt => DateTime.Now;

        public string? CreatedBy { get; set; }
    }
}
