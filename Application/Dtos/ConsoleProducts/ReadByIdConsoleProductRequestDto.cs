using Application.Dtos.Common.Interfaces;

namespace Application.Dtos.ConsoleProducts
{
    public class ReadByIdConsoleProductRequestDto : ReadByIdRequestDto
    {
        public int Id { get; set; }
    }
}
