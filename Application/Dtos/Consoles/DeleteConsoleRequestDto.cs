using Application.Dtos.Common;

namespace Application.Dtos.Consoles
{
    public class DeleteConsoleRequestDto : DeleteRequestDto
    {
        public int Id { get; set; }
    }
}
