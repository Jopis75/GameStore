using GameStore.Application.Dtos.General;

namespace GameStore.Infrastructure.IO
{
    public static class GameStoreCsvFileParser
    {
        public static IEnumerable<GameStoreFileUploadRequestDto> Parse(string[] rows)
        {
            var gameStoreFileUploadRequestDtos = new List<GameStoreFileUploadRequestDto>();

            foreach (var row in rows)
            {
                gameStoreFileUploadRequestDtos.Add(Parse(row));
            }

            return gameStoreFileUploadRequestDtos;
        }

        public static GameStoreFileUploadRequestDto Parse(string row)
        {
            var columns = row.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            // Parse all file data.
            var videoGameTitle = columns[0];
            var developerName = columns[1]; // Must exist in db.
            var publisherName = columns[2]; // Must exist in db.
            var consoleName = columns[3]; // Must exist in db.
            var genreNames = columns[4].Split('|', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries); // Must exist in db.
            var videoGameReleaseDate = DateTime.Parse(columns[5]);
            var videoGamePurchaseDate = DateTime.Parse(columns[6]);
            var videoGamePrice = Decimal.Parse(columns[7]);

            var gameStoreFileUploadRequestDto = new GameStoreFileUploadRequestDto
            {
                VideoGameTitle = videoGameTitle,
                DeveloperName = developerName,
                PublisherName = publisherName,
                ConsoleName = consoleName,
                //GenreNames = genreNames,
                VideoGameReleaseDate = videoGameReleaseDate,
                VideoGamePurchaseDate = videoGamePurchaseDate,
                //VideoGamePrice = videoGamePrice
            };

            return gameStoreFileUploadRequestDto;
        }
    }
}
