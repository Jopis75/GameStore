namespace Application.Dtos.General
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

        public string VideoGamePurchaseDate { get; set; } = String.Empty;

        public string VideoGameReleaseDate { get; set; } = String.Empty;

        public string VideoGameTitle { get; set; } = String.Empty;
    }
}
