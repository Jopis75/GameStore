using Application.Dtos.Common;

namespace Application.Dtos.VideoGames
{
    public class ReadMostPlayedVideoGameByConsoleIdRequestDto : RequestDto
    {
        public int ConsoleId { get; set; }
    }
}
