namespace Application.Dtos.Common
{
    public class DeleteRequestDto : RequestDto
    {
        public static DateTime DeletedAt => DateTime.Now;

        public string? DeletedBy { get; set; }

        public int Id { get; set; }
    }
}
