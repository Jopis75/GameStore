namespace Domain.Dtos
{
    public class ConsoleVideoGameDto : DtoBase
    {
        public ConsoleDto Console { get; set; } = new();

        public int ConsoleId { get; set; }

        public VideoGameDto VideoGame { get; set; } = new();

        public int VideoGameId { get; set; }
    }
}
