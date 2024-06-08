namespace Domain.Dtos
{
    public class ConsoleDto : ProductDto
    {
        public List<ConsoleVideoGameDto> ConsoleVideoGames { get; set; } = new();
    }
}
