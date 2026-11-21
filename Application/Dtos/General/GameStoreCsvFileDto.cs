namespace GameStore.Application.Dtos.General
{
    public class GameStoreCsvFileDto
    {
        // Must exist in db.
        public string ConsoleName { get; set; } = String.Empty;

        // Must exist in db.
        public string DeveloperName { get; set; } = String.Empty;

        // Must exist in db.
        public List<string> GenreNames { get; set; } = [];

        // Must exist in db.
        public string PublisherName { get; set; } = String.Empty;

        public decimal VideoGamePrice { get; set; }

        public DateTime VideoGamePurchaseDate { get; set; }

        public DateTime VideoGameReleaseDate { get; set; }

        public string VideoGameTitle { get; set; } = String.Empty;
    }
}
