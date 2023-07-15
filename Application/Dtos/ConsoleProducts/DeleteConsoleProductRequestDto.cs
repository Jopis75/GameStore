using Application.Dtos.Common;

namespace Application.Dtos.ConsoleProducts
{
    public class DeleteConsoleProductRequestDto : DeleteRequestDto
    {
        public int Id { get; set; }
    }
}
