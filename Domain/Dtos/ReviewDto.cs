namespace Domain.Dtos
{
    public class ReviewDto : DtoBase
    {
        public ConsoleDto? Console { get; set; } = new();

        public int? ConsoleId { get; set; }

        // Grade between 0 and 100.
        public int Grade { get; set; }

        public DateTime ReviewDate { get; set; }

        public string ReviewText { get; set; } = default!;

        public VideoGameDto? VideoGame { get; set; } = new();

        public int? VideoGameId { get; set; }
    }
}
