using Application.Dtos.Common.Interfaces;

namespace Application.Dtos.ConsoleProducts
{
    public class DeleteConsoleProductRequestDto : IDeleteRequestDto
    {
        public int Id { get; set; }
    }
}
