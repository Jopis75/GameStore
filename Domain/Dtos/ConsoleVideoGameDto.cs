namespace Domain.Dtos
{
    public class ConsoleVideoGameDto : DtoBase
    {
        public ConsoleDto Console { get; set; } = default!;

        public int ConsoleId { get; set; }

        public VideoGameDto VideoGame { get; set; } = default!;

        public int VideoGameId { get; set; }
    }
}
