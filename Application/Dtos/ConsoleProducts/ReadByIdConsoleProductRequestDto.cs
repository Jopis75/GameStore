using Application.Dtos.Common.Interfaces;

namespace Application.Dtos.ConsoleProducts
{
    public class ReadByIdConsoleProductRequestDto : IReadByIdRequestDto
    {
        public int Id { get; set; }
    }
}
