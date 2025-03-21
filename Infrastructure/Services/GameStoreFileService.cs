using Application.Dtos.General;
using Application.Exceptions;
using Application.Interfaces.Infrastructure;
using Application.Interfaces.Persistance;
using Domain.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services
{
    public class GameStoreFileService : IGameStoreFileService
    {
        private readonly ICompanyRepository _companyRepository;

        private readonly IConsoleRepository _consoleRepository;

        private readonly IConsoleVideoGameRepository _consoleVideoGameRepository;

        private readonly IGenreRepository _genreRepository;

        private readonly IVideoGameRepository _videoGameRepository;

        private readonly IVideoGameGenreRepository _videoGameGenreRepository;

        private readonly ILogger<GameStoreFileService> _logger;

        public GameStoreFileService(ICompanyRepository companyRepository, IConsoleRepository consoleRepository, IConsoleVideoGameRepository consoleVideoGameRepository, IGenreRepository genreRepository, IVideoGameRepository videoGameRepository, IVideoGameGenreRepository videoGameGenreRepository, ILogger<GameStoreFileService> logger)
        {
            _companyRepository = companyRepository;
            _consoleRepository = consoleRepository;
            _consoleVideoGameRepository = consoleVideoGameRepository;
            _genreRepository = genreRepository;
            _videoGameRepository = videoGameRepository;
            _videoGameGenreRepository = videoGameGenreRepository;
            _logger = logger;
        }

        public async Task<UploadGameStoreFileDto<VideoGameDto>> UploadAsync(IFormFile formFile, CancellationToken cancellationToken)
        {
            var memoryStream = new MemoryStream();
            await formFile.CopyToAsync(memoryStream, cancellationToken);
            memoryStream.Position = 0;

            var uploadGameStoreFileDto = await UploadAsync(memoryStream, cancellationToken);

            return uploadGameStoreFileDto;
        }

        public async Task<UploadGameStoreFileDto<VideoGameDto>> UploadAsync(Stream stream, CancellationToken cancellationToken)
        {
            var videoGameDtos = new List<VideoGameDto>();

            TextReader textReader = new StreamReader(stream);

            var rows = (await textReader.ReadToEndAsync(cancellationToken)).Split("\r\n", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            foreach (var row in rows)
            {
                //await context.Database.BeginTransactionAsync();
                try
                {
                    var columns = row.Split('|', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                    // Trim and parse all file data.
                    var videoGameTitle = columns[0];
                    var developerName = columns[1]; // Must exist in db.
                    var publisherName = columns[2]; // Must exist in db.
                    var consoleName = columns[3]; // Must exist in db.
                    var genreNames = columns[4].Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries); // Must exist in db.
                    var videoGameReleaseDate = DateTime.Parse(columns[5]);
                    var videoGamePurchaseDate = DateTime.Parse(columns[6]);
                    var videoGamePrice = Decimal.Parse(columns[7]);

                    // Create or update VideoGame.
                    var videoGameDto = await CreateOrUpdateVideoGameAsync(videoGameTitle, videoGameReleaseDate, videoGamePurchaseDate, videoGamePrice, developerName, publisherName, cancellationToken);

                    // Create if not exist ConsoleVideoGame.
                    var consoleVideoGameDtos = await CreateIfNotExistConsoleVideoGamesAsync(consoleName, videoGameDto.Id, cancellationToken);

                    // Update or insert VideoGameGenre.
                    var videoGameGenreDtos = await CreateOrUpdateVideoGameGenresAsync(videoGameDto.Id, genreNames, cancellationToken);

                    videoGameDtos.Add(videoGameDto);

                    // Log success.
                    _logger.LogInformation("Success Upsert {@VideoGameDto}.", videoGameDto);
                }
                catch (OperationCanceledException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    // Log error.
                    _logger.LogError(ex, $"Error Upsert {row}.");
                }
            }

            var uploadGameStoreFileDto = new UploadGameStoreFileDto<VideoGameDto>(videoGameDtos.ToArray(), DateTime.Now, "System");

            return uploadGameStoreFileDto;
        }

        private async Task<ConsoleVideoGameDto> CreateIfNotExistConsoleVideoGamesAsync(string consoleName, int videoGameId, CancellationToken cancellationToken)
        {
            var consoleVideoGameDtos = await _consoleVideoGameRepository.ReadByVideoGameIdAsync(videoGameId, cancellationToken);

            var consoleDtos = await _consoleRepository.ReadByNameAsync(consoleName, cancellationToken);

            // Did not found an exact match.
            if (consoleDtos.Count() != 1)
            {
                throw new NotFoundException(consoleName, consoleName);
            }

            var consoleDto = consoleDtos.First();

            if (consoleVideoGameDtos.Any(cvgd => cvgd.ConsoleId == consoleDto.Id) == false)
            {
                var consoleVideoGameDto = new ConsoleVideoGameDto
                {
                    ConsoleId = consoleDto.Id,
                    VideoGameId = videoGameId,
                    CreatedAt = DateTime.Now,
                    CreatedBy = "System",
                    DeletedAt = null,
                    DeletedBy = String.Empty,
                    UpdatedAt = null,
                    UpdatedBy = String.Empty
                };

                var createdConsoleVideoGameDto = await _consoleVideoGameRepository.CreateAsync(consoleVideoGameDto, cancellationToken);

                return createdConsoleVideoGameDto;
            }

            return new ConsoleVideoGameDto();
        }

        private async Task<VideoGameDto> CreateOrUpdateVideoGameAsync(string title, DateTime releaseDate, DateTime purchaseDate, decimal price, string developerName, string publisherName, CancellationToken cancellationToken)
        {
            var developerCompanyDtos = await _companyRepository.ReadByNameAsync(developerName, cancellationToken);

            // Did not found an exact match.
            if (developerCompanyDtos.Count() != 1)
            {
                throw new NotFoundException(developerName, developerName);
            }

            var developerId = developerCompanyDtos.First().Id;

            var publisherCompanyDtos = await _companyRepository.ReadByNameAsync(publisherName, cancellationToken);

            // Did not found an exact match.
            if (publisherCompanyDtos.Count() != 1)
            {
                throw new NotFoundException(publisherName, publisherName);
            }

            var publisherId = publisherCompanyDtos.First().Id;

            var videoGameDtos = await _videoGameRepository.ReadByTitleAsync(title, cancellationToken);

            // Create if not exist.
            if (videoGameDtos.Any() == false)
            {
                var videoGameDto = new VideoGameDto
                {
                    Title = title,
                    ReleaseDate = releaseDate,
                    PurchaseDate = purchaseDate,
                    Price = price,
                    DeveloperId = developerId,
                    PublisherId = publisherId,
                    CreatedAt = DateTime.Now,
                    CreatedBy = "System",
                    DeletedAt = null,
                    DeletedBy = String.Empty,
                    UpdatedAt = null,
                    UpdatedBy = String.Empty
                };

                var createdVideoGameDto = await _videoGameRepository.CreateAsync(videoGameDto, cancellationToken);

                return createdVideoGameDto;
            }
            else // Update if exist.
            {
                var videoGameDto = videoGameDtos.First();
                videoGameDto.Title = title;
                videoGameDto.ReleaseDate = releaseDate;
                videoGameDto.PurchaseDate = purchaseDate;
                videoGameDto.Price = price;
                videoGameDto.DeveloperId = developerId;
                videoGameDto.PublisherId = publisherId;
                videoGameDto.UpdatedAt = DateTime.Now;
                videoGameDto.UpdatedBy = "System";

                var updatedVideoGameDto = await _videoGameRepository.UpdateAsync(videoGameDto, cancellationToken);

                return updatedVideoGameDto;
            }
        }

        private async Task<IEnumerable<VideoGameGenreDto>> CreateOrUpdateVideoGameGenresAsync(int videoGameId, string[] genreNames, CancellationToken cancellationToken)
        {
            var videoGameGenreDtos = await _videoGameGenreRepository.ReadByVideoGameIdAsync(videoGameId, cancellationToken);

            foreach (var videoGameGenreDto in videoGameGenreDtos)
            {
                await _videoGameGenreRepository.DeleteAsync(videoGameGenreDto, cancellationToken);
            }

            var createdVideoGameGenreDtos = new List<VideoGameGenreDto>();

            foreach (var genreName in genreNames)
            {
                var genreDtos = await _genreRepository.ReadByNameAsync(genreName, cancellationToken);

                // Did not found exact match.
                if (genreDtos.Count() != 1)
                {
                    throw new NotFoundException(genreName, genreName);
                }

                var genreDto = genreDtos.First();

                var videoGameGenreDto = new VideoGameGenreDto
                {
                    VideoGameId = videoGameId,
                    GenreId = genreDto.Id,
                    CreatedAt = DateTime.Now,
                    CreatedBy = "System",
                    DeletedAt = null,
                    DeletedBy = String.Empty,
                    UpdatedAt = null,
                    UpdatedBy = String.Empty
                };

                var createdVideoGameGenreDto = await _videoGameGenreRepository.CreateAsync(videoGameGenreDto, cancellationToken);

                createdVideoGameGenreDtos.Add(createdVideoGameGenreDto);
            }

            return createdVideoGameGenreDtos;
        }
    }
}
