using Application.Dtos.Common;

namespace Application.Dtos.ConsoleVideoGames
{
    public class CreateConsoleVideoGameRequestDto : CreateRequestDto
    {
        public int ConsoleId { get; set; }

        public int VideoGameId { get; set; }
    }
}
