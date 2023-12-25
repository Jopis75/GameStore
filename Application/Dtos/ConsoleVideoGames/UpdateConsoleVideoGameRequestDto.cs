using Application.Dtos.Common;

namespace Application.Dtos.ConsoleVideoGames
{
    public class UpdateConsoleVideoGameRequestDto : UpdateRequestDto
    {
        public int ConsoleId { get; set; }

        public int VideoGameId { get; set; }
    }
}
