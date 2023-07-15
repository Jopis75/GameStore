using Application.Dtos.Common;

namespace Application.Dtos.Consoles
{
    public class ReadByIdConsoleRequestDto : ReadByIdRequestDto
    {
        public int Id { get; set; }
    }
}
