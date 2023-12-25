using Application.Dtos.Common;
using Application.Dtos.Consoles;
using Application.Dtos.VideoGames;

namespace Application.Dtos.ConsoleVideoGames
{
    public class ReadByIdConsoleVideoGameResponseDto : ReadByIdResponseDto
    {
        public ReadByIdConsoleResponseDto Console { get; set; } = new();

        public ReadByIdVideoGameResponseDto VideoGame { get; set; } = new();

    }
}
