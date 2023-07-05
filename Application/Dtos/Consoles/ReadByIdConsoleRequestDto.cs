using Application.Dtos.Common.Interfaces;

namespace Application.Dtos.Consoles
{
    public class ReadByIdConsoleRequestDto : IReadByIdRequestDto
    {
        public int Id { get; set; }
    }
}
