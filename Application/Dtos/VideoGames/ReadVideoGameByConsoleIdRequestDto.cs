using Application.Dtos.Common;

namespace Application.Dtos.VideoGames
{
    public class ReadVideoGameByConsoleIdRequestDto : RequestDto
    {
        public int ConsoleId { get; set; }
    }
}
