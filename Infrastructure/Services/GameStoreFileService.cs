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

        public async Task<UploadGameStoreFileDto<VideoGameDto>> UpsertAsync(IFormFile formFile, CancellationToken cancellationToken)
        {
            var videoGameDtos = new List<VideoGameDto>();

            var memoryStream = new MemoryStream();
            await formFile.CopyToAsync(memoryStream, cancellationToken);
            memoryStream.Position = 0;

            TextReader textReader = new StreamReader(memoryStream);
            var rows = (await textReader.ReadToEndAsync()).Split("\r\n");

            foreach (var row in rows)
            {
                try
                {
                    var columns = row.Split('|');

                    // Trim and parse all file data.
                    var videoGameTitle = columns[0].Trim();
                    var consoleName = columns[1].Trim(); // Must exist in db.
                    var developerName = columns[2].Trim(); // Must exist in db.
                    var genreNames = columns[3].Trim().Split(';'); // Must exist in db.
                    var videoGameReleaseDate = DateTime.Parse(columns[4].Trim());
                    var videoGamePurchaseDate = DateTime.Parse(columns[5].Trim());
                    var videoGamePrice = Decimal.Parse(columns[6].Trim());

                    // Update or insert VideoGame.
                    var videoGameDto = await UpsertVideoGameAsync(videoGameTitle, videoGameReleaseDate, videoGamePurchaseDate, videoGamePrice, developerName, cancellationToken);

                    // Update or insert ConsoleVideoGame.
                    var consoleVideoGameDtos = await UpsertConsoleVideoGamesAsync(consoleName, videoGameDto.Id, cancellationToken);

                    // Update or insert VideoGameGenre.
                    var videoGameGenreDtos = await UpsertVideoGameGenresAsync(videoGameDto.Id, genreNames, cancellationToken);

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

        public Task<UploadGameStoreFileDto<VideoGameDto>> UpsertAsync(Stream stream, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private async Task<ConsoleVideoGameDto> UpsertConsoleVideoGamesAsync(string consoleName, int videoGameId, CancellationToken cancellationToken)
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

        private async Task<IEnumerable<VideoGameGenreDto>> UpsertVideoGameGenresAsync(int videoGameId, string[] genreNames, CancellationToken cancellationToken)
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

        private async Task<VideoGameDto> UpsertVideoGameAsync(string title, DateTime releaseDate, DateTime purchaseDate, decimal price, string developerName, CancellationToken cancellationToken)
        {
            var videoGameDtos = await _videoGameRepository.ReadByTitleAsync(title, cancellationToken);

            var companyDtos = await _companyRepository.ReadByNameAsync(developerName, cancellationToken);

            // Did not found an exact match.
            if (companyDtos.Count() != 1)
            {
                throw new NotFoundException(developerName, developerName);
            }

            var companyDto = companyDtos.First();

            // Create if not exist.
            if (videoGameDtos.Count() == 0)
            {
                var videoGameDto = new VideoGameDto
                {
                    Title = title,
                    ReleaseDate = releaseDate,
                    PurchaseDate = purchaseDate,
                    Price = price,
                    DeveloperId = companyDto.Id,
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
                videoGameDto.DeveloperId = companyDto.Id;
                videoGameDto.UpdatedAt = DateTime.Now;
                videoGameDto.UpdatedBy = "System";

                var updatedVideoGameDto = await _videoGameRepository.UpdateAsync(videoGameDto, cancellationToken);

                return updatedVideoGameDto;
            }
        }
    }
}
