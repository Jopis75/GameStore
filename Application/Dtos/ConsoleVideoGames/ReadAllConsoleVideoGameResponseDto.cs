using Application.Dtos.Common;
using Application.Dtos.Consoles;
using Application.Dtos.VideoGames;

namespace Application.Dtos.ConsoleVideoGames
{
    public class ReadAllConsoleVideoGameResponseDto : ReadAllResponseDto
    {
        public ReadAllConsoleResponseDto Console { get; set; } = new();

        public ReadAllVideoGameResponseDto VideoGame { get; set; } = new();
    }
}
