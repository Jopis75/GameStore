using Application.Dtos.Common.Interfaces;

namespace Application.Dtos.Consoles
{
    public class ReadByIdConsoleRequestDto : ReadByIdRequestDto
    {
        public int Id { get; set; }
    }
}
