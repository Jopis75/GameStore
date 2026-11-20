namespace GameStore.Application.Dtos.General
{
    public class GameStoreFileUploadRequestDto
    {
        // Must exist in db.
        public string ConsoleName { get; set; } = String.Empty;

        // Must exist in db.
        public string DeveloperName { get; set; } = String.Empty;

        // Must exist in db.
        public string GenreNames { get; set; } = String.Empty;

        // Must exist in db.
        public string PublisherName { get; set; } = String.Empty;

        public string VideoGamePrice { get; set; } = String.Empty;

        public DateTime VideoGamePurchaseDate { get; set; }

        public DateTime VideoGameReleaseDate { get; set; }

        public string VideoGameTitle { get; set; } = String.Empty;
    }
}
