using GameStore.Application.Dtos.General;
using GameStore.Application.Interfaces.Infrastructure;
using GameStore.Application.Interfaces.Persistance;

namespace GameStore.Infrastructure.IO
{
    public class GameStoreCsvFileParser(IUnitOfWork unitOfWork) : IGameStoreCsvFileParser
    {
        public async Task<IEnumerable<GameStoreCsvFileDto>> ParseAsync(string[] rows, CancellationToken cancellationToken)
        {
            var gameStoreCsvFileDtos = new List<GameStoreCsvFileDto>();

            foreach (var row in rows)
            {
                var gameStoreCsvFileDto = await ParseAsync(row, cancellationToken);
                gameStoreCsvFileDtos.Add(gameStoreCsvFileDto);
            }

            return gameStoreCsvFileDtos;
        }

        public async Task<GameStoreCsvFileDto> ParseAsync(string row, CancellationToken cancellationToken)
        {
            var columns = row.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            if (columns.Length != 8)
            {
                throw new ArgumentException();
            }

            // Parse the videogame titel.
            var videoGameTitle = columns[0];

            if (String.IsNullOrWhiteSpace(videoGameTitle))
            {
                throw new ArgumentException();
            }

            // Parse the developer name. The developer company name must exist in db.
            var developerName = columns[1];

            if (String.IsNullOrWhiteSpace(developerName))
            {
                throw new ArgumentException();
            }

            var developerCompany = await unitOfWork.CompanyRepository.ReadByNameExactAsync(developerName, cancellationToken);

            if (developerCompany == null)
            {
                throw new ArgumentException();
            }

            // Parse the publisher name. The publisher company name must exist in db.
            var publisherName = columns[2];

            if (String.IsNullOrWhiteSpace(publisherName))
            {
                throw new ArgumentException();
            }

            var publisherCompany = await unitOfWork.CompanyRepository.ReadByNameExactAsync(publisherName, cancellationToken);

            if (publisherCompany == null)
            {
                throw new ArgumentException();
            }

            // Parse the console name. The console name must exist in db.
            var consoleName = columns[3];

            if (String.IsNullOrWhiteSpace(consoleName))
            {
                throw new ArgumentException();
            }

            var console = await unitOfWork.ConsoleRepository.ReadByNameExactAsync(consoleName, cancellationToken);

            if (console == null)
            {
                throw new ArgumentException();
            }

            // Parse the gere names. The genre names must exist in db.
            var genreNames = columns[4].Split('|', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            if (genreNames.Length == 0)
            {
                throw new ArgumentException();
            }

            foreach (var genreName in genreNames)
            {
                var genre = unitOfWork.GenreRepository.ReadByNameExactAsync(genreName, cancellationToken);

                if (genre == null)
                {
                    throw new ArgumentException();
                }
            }

            // Parse the videogame release date.
            if (!DateTime.TryParse(columns[5], out var videoGameReleaseDate))
            {
                throw new ArgumentException();
            }

            // Parse the videogame purchase date.
            if (!DateTime.TryParse(columns[6], out var videoGamePurchaseDate))
            {
                throw new ArgumentException();
            }

            // Parse the videogame price.
            if (!Decimal.TryParse(columns[7], out var videoGamePrice))
            {
                throw new ArgumentException();
            }

            var gameStoreCsvFileDto = new GameStoreCsvFileDto
            {
                VideoGameTitle = videoGameTitle,
                DeveloperName = developerName,
                PublisherName = publisherName,
                ConsoleName = consoleName,
                GenreNames = [.. genreNames],
                VideoGameReleaseDate = videoGameReleaseDate,
                VideoGamePurchaseDate = videoGamePurchaseDate,
                VideoGamePrice = videoGamePrice
            };

            return gameStoreCsvFileDto;
        }
    }
}
