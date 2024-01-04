using Application.Dtos.Common;
using Application.Dtos.Consoles;
using Application.Dtos.VideoGames;

namespace Application.Dtos.ConsoleVideoGames
{
    public class ReadConsoleVideoGameResponseDto : ReadResponseDto
    {
        public ReadConsoleResponseDto Console { get; set; } = new();

        public ReadVideoGameResponseDto VideoGame { get; set; } = new();
    }
}
