using Application.Dtos.Common.Interfaces;

namespace Application.Dtos.ConsoleProducts
{
    public class DeleteConsoleProductRequestDto : DeleteRequestDto
    {
        public int Id { get; set; }
    }
}
