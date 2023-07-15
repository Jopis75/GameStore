using Application.Dtos.Common.Interfaces;

namespace Application.Dtos.Consoles
{
    public class DeleteConsoleRequestDto : DeleteRequestDto
    {
        public int Id { get; set; }
    }
}
