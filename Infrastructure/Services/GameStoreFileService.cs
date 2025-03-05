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

        private readonly IGenreRepository _genreRepository;

        private readonly IVideoGameRepository _videoGameRepository;

        private readonly ILogger<GameStoreFileService> _logger;

        public GameStoreFileService(ICompanyRepository companyRepository, IConsoleRepository consoleRepository, IGenreRepository genreRepository, IVideoGameRepository videoGameRepository, ILogger<GameStoreFileService> logger)
        {
            _companyRepository = companyRepository;
            _consoleRepository = consoleRepository;
            _genreRepository = genreRepository;
            _videoGameRepository = videoGameRepository;
            _logger = logger;
        }

        public async Task UpsertAsync(IFormFile formFile, CancellationToken cancellationToken)
        {
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

                    var videoGameDto = await UpsertVideoGameAsync(videoGameTitle, videoGameReleaseDate, videoGamePurchaseDate, videoGamePrice, cancellationToken);

                    // Log success.
                    _logger.LogInformation("Upsert {@VideoGameDto}.", videoGameDto);
                }
                catch (OperationCanceledException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    // Log error.
                    _logger.LogError(ex, "Error Upsert {@Row}.", row);
                }
            }
        }

        public Task UpsertAsync(Stream stream)
        {
            return Task.FromResult(0);
        }

        private async Task<CompanyDto> GetCompanyDtoAsync(string name, CancellationToken cancellationToken)
        {
            var companyDtos = await _companyRepository.ReadByNameAsync(name, cancellationToken);

            if (companyDtos.Count() != 1)
            {
                throw new NotFoundException(name, name);
            }

            return companyDtos.First();
        }

        private async Task<ConsoleVideoGameDto> GetConsoleVideoGameDtoAsync(string consoleName, int videoGameId, CancellationToken cancellationToken)
        {
            var consoleDtos = await _consoleRepository.ReadByNameAsync(consoleName, cancellationToken);

            if (consoleDtos.Count() != 1)
            {
                throw new NotFoundException(consoleName, consoleName);
            }

            var consoleVideoGameDto = new ConsoleVideoGameDto
            {
                ConsoleId = consoleDtos.First().Id,
                VideoGameId = videoGameId,
                CreatedAt = DateTime.Now,
                CreatedBy = "System",
                DeletedAt = null,
                DeletedBy = String.Empty,
                UpdatedAt = null,
                UpdatedBy = String.Empty
            };

            return consoleVideoGameDto;
        }

        private async Task<List<VideoGameGenreDto>> GetVideoGameGenreDtosAsync(int videoGameId, string[] genreNames, CancellationToken cancellationToken)
        {
            var videoGameGenreDtos = new List<VideoGameGenreDto>();

            foreach (var genreName in genreNames)
            {
                var genreDtos = await _genreRepository.ReadByNameAsync(genreName, cancellationToken);

                if (genreDtos.Count() != 1)
                {
                    throw new NotFoundException(genreName, genreName);
                }

                videoGameGenreDtos.Add(new VideoGameGenreDto
                {
                    VideoGameId = videoGameId,
                    GenreId = genreDtos.First().Id,
                    CreatedAt = DateTime.Now,
                    CreatedBy = "System",
                    DeletedAt = null,
                    DeletedBy = String.Empty,
                    UpdatedAt = null,
                    UpdatedBy = String.Empty
                });
            }

            return videoGameGenreDtos;
        }

        private async Task<VideoGameDto> UpsertVideoGameAsync(string title, DateTime releaseDate, DateTime purchaseDate, decimal price, CancellationToken cancellationToken)
        {
            var videoGameDtos = await _videoGameRepository.ReadByTitleAsync(title, cancellationToken);

            var videoGameDtoCount = videoGameDtos.Count();

            // Create if not exist.
            if (videoGameDtoCount == 0)
            {
                var videoGameDto = new VideoGameDto
                {
                    Title = title,
                    ReleaseDate = releaseDate,
                    PurchaseDate = purchaseDate,
                    Price = price,
                    CreatedAt = DateTime.Now,
                    CreatedBy = "System",
                    DeletedAt = null,
                    DeletedBy = String.Empty,
                    UpdatedAt = null,
                    UpdatedBy = String.Empty
                };

                return await _videoGameRepository.CreateAsync(videoGameDto, cancellationToken);
            }
            else // Update if exist.
            {
                var videoGameDto = new VideoGameDto
                {
                    Id = videoGameDtos.First().Id,
                    Title = title,
                    ReleaseDate = releaseDate,
                    PurchaseDate = purchaseDate,
                    Price = price,
                    UpdatedAt = DateTime.Now,
                    UpdatedBy = "System"
                };

                return await _videoGameRepository.UpdateAsync(videoGameDto, cancellationToken);
            }
        }
    }
}
