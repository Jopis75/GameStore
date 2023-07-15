using Application.Dtos.Common;

namespace Application.Dtos.ConsoleProducts
{
    public class ReadByIdConsoleProductRequestDto : ReadByIdRequestDto
    {
        public int Id { get; set; }
    }
}
