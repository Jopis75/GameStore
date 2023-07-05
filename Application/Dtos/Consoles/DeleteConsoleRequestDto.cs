using Application.Dtos.Common.Interfaces;

namespace Application.Dtos.Consoles
{
    public class DeleteConsoleRequestDto : IDeleteRequestDto
    {
        public int Id { get; set; }
    }
}
