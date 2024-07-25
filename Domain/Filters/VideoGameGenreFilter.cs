namespace Domain.Filters
{
    public class VideoGameGenreFilter : FilterBase
    {
        public int? GenreId { get; set; }

        public int? VideoGameId { get; set; }
    }
}
