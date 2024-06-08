namespace Domain.Dtos
{
    public class ConsoleVideoGameDto : DtoBase
    {
        public ConsoleDto Console { get; set; } = new();

        public VideoGameDto VideoGame { get; set; } = new();
    }
}
